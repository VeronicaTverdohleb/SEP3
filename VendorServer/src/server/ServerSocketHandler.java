package server;

import model.Model;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;

import javax.xml.crypto.Data;
import java.io.*;
import java.net.Socket;
import java.nio.charset.StandardCharsets;

public class ServerSocketHandler implements Runnable {
    private Socket socket;
    private InputStream inFromClient;
    private OutputStream outToClient;

    private Model model;

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

    @Override
    public void run() {
        try {
            while (true) {
                int messageLength = inFromClient.read();
                byte[] message = new byte[messageLength];
                inFromClient.readNBytes(message, 0, messageLength);

                JSONObject result = convertByteIntoJSONObject(message);
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
            e.printStackTrace();
        }
    }

    public void sendData(byte[] jsonToSend) {
        try {
            int length = jsonToSend.length;
            outToClient.write(length);
            outToClient.write(jsonToSend, 0, length);
        } catch (IOException e) {
            e.printStackTrace();
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
