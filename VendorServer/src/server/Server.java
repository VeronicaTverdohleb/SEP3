package server;

import datamodel.DataModel;
import model.Model;
import datamodel.DataModelManager;

import model.ModelManager;

import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;
import java.sql.SQLException;

public class Server {

    public void startServer() {
        System.out.println("Starting server...");
        try{
            ServerSocket serverSocket = new ServerSocket(2910);
            DataModel dataModel = new DataModelManager();
            Model model = new ModelManager(dataModel);

            while(true){
                Socket socket = serverSocket.accept();
                System.out.println("Client connected");

                ServerSocketHandler ssh = new ServerSocketHandler(socket, model);
                Thread t = new Thread(ssh);
                t.start();
            }
        }catch(IOException e){
            e.printStackTrace();
        } catch (SQLException e) {
            throw new RuntimeException(e);
        }
    }
}
