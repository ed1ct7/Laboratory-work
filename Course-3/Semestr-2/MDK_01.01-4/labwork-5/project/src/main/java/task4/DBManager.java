package task4;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.io.File;

public class DBManager {
    private static DBManager instance;
    private Connection conn;

    private DBManager() {
        try {
            File dbfile = new File("sqlite/test.db");
            dbfile.getParentFile().mkdirs();
            dbfile.createNewFile();
            conn = DriverManager.getConnection("jdbc:sqlite:" + dbfile.getPath());
            System.out.println("Database connected");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public static DBManager getInstance() {
        if (instance == null) instance = new DBManager();
        return instance;
    }

    public Connection getConnection() {
        return conn;
    }


}
