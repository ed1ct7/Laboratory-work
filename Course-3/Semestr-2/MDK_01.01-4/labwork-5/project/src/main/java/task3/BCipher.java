package task3;

import java.lang.classfile.instruction.CharacterRange;

public class BCipher implements ICipher {
    public static final String ALPHABET = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
    @Override
    public String encode(String str) {
        String encoded = "";
        for(Character i : str.toCharArray()){
            int CharIndex = (ALPHABET.length() - 1) - ALPHABET.indexOf(i);
            encoded += ALPHABET.charAt(CharIndex);
        }
        return encoded;
    }
    @Override
    public String decode(String str) {
        String decoded = "";
        for(Character i : str.toCharArray()){
            int CharIndex = (ALPHABET.length() - 1) - ALPHABET.indexOf(i);
            decoded += ALPHABET.charAt(CharIndex);
        }
        return decoded;
    }
}
