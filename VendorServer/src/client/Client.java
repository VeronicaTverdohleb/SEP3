package client;

import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;

import java.io.*;
import java.net.Socket;
import java.nio.ByteBuffer;
import java.nio.charset.StandardCharsets;
import java.util.HashMap;
import java.util.Map;
import java.util.Scanner;

public class Client {
    private OutputStream outToServer;
    private InputStream inFromServer;

    public void startClient() {
        try{
            Socket socket = new Socket("localhost", 2910);
            System.out.println("Connected to the server");

            outToServer = socket.getOutputStream();
            inFromServer = socket.getInputStream();

            Thread t = new Thread(this::listenToMessages);
            t.setDaemon(true);
            t.start();

            Scanner scanner = new Scanner(System.in);

            while (true) {
                System.out.println("Please type ingredient to search for >");
                String scanned = scanner.nextLine();
                if (scanned.equals("exit")) {
                    socket.close();
                    System.out.println("Client exits");
                    break;
                }

                Map<String, String> map = new HashMap<>();
                map.put("Action", "get");
                map.put("IngredientName", scanned);

                // Map into JSON, so it has "" around keys and values (otherwise it just makes the values as Ingredient=Tomato)
                JSONObject json = new JSONObject(map);

                System.out.println("Here is the string that will be sent to Server: " + json);

                // Prepare byte array for the message content
                byte[] message = json.toString().getBytes(StandardCharsets.UTF_8);

                int toSendLen = message.length;             // Get message length as integer
                // Prepare byte array that holds the size of the message
                byte[] toSendLenBytes = new byte[4];        // Make a Byte array of 4 positions
                // Integer is 32-bit, so we can split it into 4 x byte (1 byte = 8 bit)
                // And put it into a Byte array in the first 4 positions (see the shifting below)
                // If we wanted a larger value, for example 64-bit long, we'd make an array of 8 spots
                // and shifted 8 times
                toSendLenBytes[0] = (byte)(toSendLen & 0xff);
                toSendLenBytes[1] = (byte)((toSendLen >> 8) & 0xff);
                toSendLenBytes[2] = (byte)((toSendLen >> 16) & 0xff);
                toSendLenBytes[3] = (byte)((toSendLen >> 24) & 0xff);

                outToServer.write(toSendLenBytes);  // First, we send byte array with the size of the message
                outToServer.write(message);         // Then we send byte array with the message content
            }

        } catch (IOException e){
            e.printStackTrace();
        }
    }

    public void listenToMessages() {
        while (true) {
            try
            {
                // NOTE: explanation to the code below is in ServerSocketHolder
                byte[] lenBytes = new byte[4];
                inFromServer.read(lenBytes, 0, 4);
                int len = (((lenBytes[3] & 0xff) << 24) | ((lenBytes[2] & 0xff) << 16) |
                        ((lenBytes[1] & 0xff) << 8) | (lenBytes[0] & 0xff));
                byte[] receivedBytes = new byte[len];
                inFromServer.read(receivedBytes, 0, len);

                JSONObject result = convertByteIntoJSONObject(receivedBytes);
                System.out.println("Received from Server: " + result);
            }
            catch (IOException e)
            {
                e.printStackTrace();
            }
        }
    }

    public JSONObject convertByteIntoJSONObject(byte[] result) {
        JSONParser parser = new JSONParser();
        JSONObject json = null;
        try {
            String newString = new String(result, StandardCharsets.UTF_8);
            json = (JSONObject) parser.parse(newString);
        } catch (ParseException e) {
            throw new RuntimeException(e);
        }
        return json;
    }
}
