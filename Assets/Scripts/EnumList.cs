public enum Owner {
    PLAYER,
    COM,
    NONE,
}

public class Buff {
    public float rate;
    public int count;

    public Buff(float _rate, int _count) {
        rate = _rate;
        count = _count;
    }

    public void CountDown() {
        count--;
    }
}

public enum CardType {
    ATTACK,
    GUARD,
    BUFF,
    COUNTER,
    RECOVERY,
}