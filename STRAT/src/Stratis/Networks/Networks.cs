using NBitcoin;

namespace Stratis.Networks
{
   public static class Networks
   {
      public static NetworksSelector Stratis
      {
         get
         {
            return new NetworksSelector(() => new StratisMain(), () => new StratisTest(), () => new StratisRegTest());
         }
      }
   }
}
