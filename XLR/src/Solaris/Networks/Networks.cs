using NBitcoin;

namespace Solaris.Networks
{
   public static class Networks
   {
      public static NetworksSelector Solaris
      {
         get
         {
            return new NetworksSelector(() => new SolarisMain(), () => new SolarisTest(), () => new SolarisRegTest());
         }
      }
   }
}
