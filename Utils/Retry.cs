using System;
using System.Threading;

public static class Retry {
    public static void Do(Action action, int attempts, TimeSpan delay){
        Exception last = null;
        for(int i=0;i<attempts;i++){
            try { action(); return; }
            catch(Exception ex){ last = ex; Thread.Sleep(delay); }
        }
        throw last ?? new Exception("Retry failed");
    }
}
