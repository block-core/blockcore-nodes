using Blockcore.Networks;
using NBitcoin;

namespace AMS.Networks
{
   public static class Networks
   {
      public static NetworksSelector AMS
      {
         get
         {
            return new NetworksSelector(() => new AMSMain(), () => new AMSTest(), () => new AMSRegTest());
         }
      }
   }
}
