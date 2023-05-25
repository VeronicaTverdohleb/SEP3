package server;

import model.Model;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;

import javax.xml.crypto.Data;
import java.io.*;
import java.net.Socket;
import java.net.SocketException;
import java.nio.ByteBuffer;
import java.nio.charset.StandardCharsets;

/**
 * Implements Runnable
 * Responsible for sending the Data and converting into byte
 */
public class ServerSocketHandler implements Runnable {
    private Socket socket;
    private InputStream inFromClient;
    private OutputStream outToClient;

    private Model model;

    /**
     * Initializes the streams, sockets and Model
     * @param socket Socket
     * @param model Model
     */
    public ServerSocketHandler(Socket socket, Model model) {
        this.socket = socket;
        this.model = model;
        try {
            inFromClient = socket.getInputStream();
            outToClient = socket.getOutputStream();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    /**
     * Method which gets the data from Client into byte
     */
    @Override
    public void run() {
        try {
            while (true) {
                // First received a byte array thta holds an integer with the content size
                // So we prepare a byte array with 4 positions (32 bit = 4 bytes => 1 byte is in 1 position of array)
                byte[] lenBytes = new byte[4];

                // First message holds the length of the following message
                // By calling this "read" method, the message of byte array length 4 will be saved into "lenBytes"
                inFromClient.read(lenBytes, 0, 4);
                // Take the first 4 positions of the byte array "lenBytes" and add them together
                // The fancy "& 0xff" determines/specifies that it is a byte we read in each position (0xff = 255 = 8 bit = 1 byte)
                // It is to avoid cluttering the buffer and cut off all bits larger than a byte in the array position
                // The bit-shifting part "<< 24" determines which part of the byte we read
                int len = (((lenBytes[3] & 0xff) << 24) | ((lenBytes[2] & 0xff) << 16) |
                        ((lenBytes[1] & 0xff) << 8) | (lenBytes[0] & 0xff));

                // Now we know the length of the message content, so we prepare a byte array of that length
                byte[] receivedBytes = new byte[len];

                // Second message holds the actual data (JSON with Action and Ingredient name)
                inFromClient.read(receivedBytes, 0, len);

                JSONObject result = convertByteIntoJSONObject(receivedBytes);
                System.out.println("Received from client: " + result.toString());

                if (result.get("Action").equals("get".toLowerCase())) {
                    if (result.containsKey("IngredientName")) {
                        byte[] newObjectToSend = model.getVendors(result.get("IngredientName").toString());
                        sendData(newObjectToSend);
                    }
                } else if (result.get("Action").equals("exit".toLowerCase())) {
                    socket.close();
                    System.out.println("Client disconnected");
                    break;
                }
            }
        } catch (IOException e) {
            try {
                socket.close();
                System.out.println("Client disconnected");
            } catch (IOException ex) {
                throw new RuntimeException(ex);
            }
        }
    }

    /**
     * Method for sending data to client
     * @param bytesToSend the bytes to send
     */
    public void sendData(byte[] bytesToSend) {
        try {
            int toSendLen = bytesToSend.length;              // Get message length as integer

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
            outToClient.write(toSendLenBytes);          // First, we send byte array with the size of the message
            outToClient.write(bytesToSend);             // Then we send byte array with the message content

            String contentForClient = new String(bytesToSend);
            System.out.println("Sending back to Client: " + contentForClient);

        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    /**
     * Method converts byte into JSON
     * @param result the return json
     * @return json
     */
    public JSONObject convertByteIntoJSONObject(byte[] result) {
        JSONParser parser = new JSONParser();
        JSONObject json = null;
        try {
            String newString = new String(result, StandardCharsets.UTF_8);
            //System.out.println("About to parse this: " + newString);
            json = (JSONObject) parser.parse(newString);
        } catch (ParseException e) {
            throw new RuntimeException(e);
        }
        return json;
    }

}
