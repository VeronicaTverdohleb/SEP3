package client;

import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;

import java.io.*;
import java.net.Socket;
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
                System.out.println("Here is the string that will be sent: " + json);
                int length = json.toString().getBytes(StandardCharsets.UTF_8).length;
                byte[] message;
                message = json.toString().getBytes(StandardCharsets.UTF_8);

                outToServer.write(length);
                outToServer.write(message, 0, length);
            }

        } catch (IOException e){
            e.printStackTrace();
        }
    }

    public void listenToMessages() {
        while(true){
            try
            {
                int messageLength = inFromServer.read();
                byte[] message = new byte[messageLength];
                inFromServer.readNBytes(message, 0, messageLength);

                JSONObject result = convertByteIntoJSONObject(message);
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
