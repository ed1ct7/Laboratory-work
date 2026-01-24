package task4.Interfaces;

public interface IATMmachine {
    public float checkBalance();
    public void replenishBalance(float money);
    public float withdrawBalance(float money);
    public float makeTransaction(int id, float money);
}
