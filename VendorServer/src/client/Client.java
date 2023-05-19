package client;


import org.json.simple.JSONObject;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import java.util.Scanner;

public class Client {
    private ObjectOutputStream outToServer;
    private ObjectInputStream inFromServer;

    public void startClient() {
        try{
            Socket socket = new Socket("localhost", 2910);
            System.out.println("Connected to the server");

            outToServer = new ObjectOutputStream(socket.getOutputStream());
            inFromServer = new ObjectInputStream(socket.getInputStream());

            Thread t = new Thread(this::listenToMessages);
            t.setDaemon(true);
            t.start();

            Scanner scanner = new Scanner(System.in);

            while (true) {
                System.out.println("Please type ingredient to search for >");
                String scanned = scanner.nextLine();

                Map<String, String> map = new HashMap<>();
                map.put("Action","get");
                map.put("IngredientName",scanned);
                JSONObject msg = new JSONObject(map);

                outToServer.writeObject(msg);

                if (scanned.equals("exit")) {
                    socket.close();
                    System.out.println("Client exits");
                    break;
                }
            }

        } catch (IOException e){
            e.printStackTrace();
        }
    }

    public void listenToMessages() {
        while(true){
            try
            {
                JSONObject result = (JSONObject) inFromServer.readObject();
                System.out.println("Client received: " + result.toJSONString());
            }
            catch (IOException | ClassNotFoundException e)
            {
                e.printStackTrace();
            }
        }
    }
}
