package task3;

import static java.lang.IO.print;

public class Main {
    public static void main(String[] args) {
        BCipher bCipher = new BCipher();
        print(bCipher.encode("ВВВВ"));
        print(bCipher.decode("ЭЭЭЭ"));
    }
}
