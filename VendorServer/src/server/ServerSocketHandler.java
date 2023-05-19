package server;


import datamodel.DataModel;
import model.Model;
import org.json.simple.JSONObject;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;

public class ServerSocketHandler implements Runnable {
  private Socket socket;
  private ObjectInputStream inFromClient;
  private ObjectOutputStream outToClient;

  private Model model;



  public ServerSocketHandler(Socket socket, Model model) {
    this.socket = socket;
    this.model = model;
    try {
      inFromClient = new ObjectInputStream(socket.getInputStream());
      outToClient = new ObjectOutputStream(socket.getOutputStream());
    } catch (IOException e) {
      e.printStackTrace();
    }
  }

  @Override
  public void run() {
    try {
      while (true) {
        JSONObject result = null;
        try {
          result = (JSONObject) inFromClient.readObject();
        } catch (IOException | ClassNotFoundException e) {
          throw new RuntimeException(e);
        }

        String read = result.toJSONString();
        System.out.println("Received from client: " + read);

        if (result.get("Action").equals("get".toLowerCase())) {
          if (result.containsKey("IngredientName")) {
            JSONObject newObjectToSend = model.getVendors(result.get("IngredientName").toString());
            sendData(newObjectToSend);
          }
        }
        else if (result.get("Action").equals("exit".toLowerCase())) {
          socket.close();
          System.out.println("Client disconnected");
          break;
        }
      }
    } catch (IOException e) {
      e.printStackTrace();
    }
  }

  public void sendData(JSONObject jsonToSend) {
    try {
      outToClient.writeObject(jsonToSend);
    } catch (IOException e) {
      e.printStackTrace();
    }
  }

}
