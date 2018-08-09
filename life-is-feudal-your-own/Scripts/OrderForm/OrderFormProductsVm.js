function OrderFormProductsVm(data, parent)
{
    let self = this;
    self.parent = ko.observable(parent);
    self.id = ko.observable(null);
    self.orderForm_id = ko.observable();
    self.item = ko.observable({ id: 0, name: '', price:0 });
    self.itemQualityType = ko.observable({ id: 0, name: '', sell_multiplier: 1, buy_multiplier: 1 });
    self.itemQuality = ko.observable();
    self.isSelling = ko.observable(false);
    self.price = ko.observable(0);
    self.quantity = ko.observable(0);
    self.edit = ko.observable(false);
    self.confirmed = ko.observable(false);
    self.update = function (data) {
        self.confirmed(true);
        self.id(data.id);
        self.orderForm_id(data.orderForm_id);
        if (data.item) {
            self.activeItems().forEach(i => {
                if (i.id === data.item.id) {
                    self.item(i);
                }
            });
        } else {
            self.item(data.item);
        }

        self.activeQuality().forEach(i => {

            if (data.itemQualityType) {
                if (i.id === data.itemQualityType.id) {
                    self.itemQualityType(i);
                }
            } else {
                if (i.id === 1) {
                    self.itemQualityType(i);
                }
            }
        });
        self.isSelling(data.isSelling);
        self.quantity(data.quantity);
        self.price(data.price);
    };
    self.activeItems = function () {

        return self.parent().parent().allItems();
    };
    self.activeQuality = function () {
        if (self.item().id === 0) {
            return self.parent().parent().allItemQuality();
        }
        let aq = _.filter(self.parent().parent().allItemQuality(), (i) => {
            return i.Item_Id === self.item().id && (i.BuyActive && !self.isSelling() || i.SellActive && self.isSelling());
        });
        let types = _.filter(self.parent().parent().allItemTypes(), i => {
            return _.some(aq, q => {
                return q.ItemQualityType_Id === i.id;
            });
        });

        return types;
    };
    self.updatePrice = function () {
        let itemquality = null;
        let perPiece = 1;

        itemquality = _.find(self.parent().parent().allItemTypes(), function (i) {
            return i.id === self.itemQualityType().id;
        });
        if (!self.isSelling()) { 
            if (itemquality) {
                self.price((self.item().price * itemquality.sell_multiplier * self.quantity()).toFixed(0));   
            } else {
                alert('Item Product is not sold here');
            }
        }
        if (self.isSelling()) {
            if (itemquality) {

                self.price((self.item().price * itemquality.sell_multiplier * self.quantity() * itemquality.buy_multiplier).toFixed(0));
            } else {
                alert('Item Product is not sold here');
            }
            
        }
    };

    self.save = function () {
        console.log(ko.toJS(self));
        let data = {
            Id: self.id(),
            OrderForm_id: self.orderForm_id(),
            ItemQuality_id: self.itemQuality().Id,
            isSelling: self.isSelling(),
            Price: parseInt(self.price()),
            Quantity: parseInt(self.quantity())
        };
        $.post('/OrderFormProduct/SaveOrderFormProduct', data, function (data, err) {            
            
        });
    };
    if (data) {
        self.update(data);
    }
}