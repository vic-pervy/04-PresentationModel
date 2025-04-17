using UniRx;

namespace Code
{
    public class MarketButtonPresenter
    {
        public MarketButtonPresenter(ButtonView buttonView, PopupManager shopPresenter) 
        {
            buttonView.Button.onClick.AddListener(shopPresenter.CreateShopPopup);
 //           buttonView.Button.OnClickAsObservable().Subscribe(_=> shopPresenter.Show());
        }
    }
}
