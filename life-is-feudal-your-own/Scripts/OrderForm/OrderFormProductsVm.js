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
    self.update = function (data) {
        console.log(ko.toJS(self.activeItems()));
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

        return self.parent().parent().allItems;
    };
    self.activeQuality = function () {
        let aq = _.filter(self.parent().parent().allItemQuality, (i) => {
            return i.Item_Id === self.item().id && (i.BuyActive && !self.isSelling() || i.SellActive && self.isSelling());
        });
        let ids = [];
        _.forEach(aq, function (i) {
            if (!_.some(ids, function (d) { return i.ItemQualityType_id === d; })) {
                ids.push(i.ItemQualityType_id);
            }
        });
        let filteredQuality = [];
        _.forEach(self.parent().parent().itemTypes);
        return _.filter(self.parent().parent().itemTypes, function (i) { return _.some(ids, (d) => { return d === i.id; }); });
    };
    self.updatePrice = function () {
        let itemquality = null;
        let perPiece = 1;

        itemquality = _.find(self.parent().parent().itemTypes, function (i) {
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

                self.price((self.item().price * itemquality.sell_multiplier * itemquality.buy_multiplier * self.quantity()).toFixed(0));
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