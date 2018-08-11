function ItemQualityVm(data, parent) {
    var self = this;
    self.parent = ko.observable(parent);

    self.edit = ko.observable(false);
    self.id = ko.observable(null);
    self.item_id = ko.observable(self.parent.id);
    self.itemQualityType = ko.observable({ id: 0, name: '', sell_multiplier: 1, buy_multiplier: 1 });
    self.buy_active = ko.observable();
    self.sell_active = ko.observable();
    self.free = ko.observable(false);
    self.created_at = ko.observable('');
    self.updated_at = ko.observable('');
    self.overridePrice = ko.observable(null);
    self.sell_price = ko.computed(function () {
        if (self.overridePrice() !== 0 && self.overridePrice()) {
            return parseInt(self.overridePrice()).toFixed(0);
        }
        return (self.itemQualityType().sell_multiplier * self.parent().price()).toFixed(0);
    });
    self.buy_price = ko.computed(function () {
        console.log('Price', self.itemQualityType().sell_multiplier * self.parent().price() * self.itemQualityType().buy_multiplier);
        var price = self.parent().price();
        if (self.overridePrice() !== 0 && self.overridePrice()) {
            price = parseInt(self.overridePrice());
        }
        var totalBuyPrice = (self.itemQualityType().sell_multiplier * price * self.itemQualityType().buy_multiplier).toFixed(0);
        return totalBuyPrice;
    });
    self.updateItemQualityType = function () {
        var itemquality = null;
        var perPiece = 1;
        console.log('Is Selling', self.isSelling());
        console.log('Quality', self.quality());
        
        itemquality = _.find(self.parent().parent().itemTypes, function (i) {
            return i.ItemQualityType_id === self.ItemQualityType().id;
        });
        self.itemQualityType(itemquality);
    };
    self.update = function (data) {
        console.log('Parent', ko.toJS(self.parent().parent()));
        self.id(data.id);
        if (!data.item_id) {
            self.item_id(self.parent().id());
        } else {
            self.item_id(data.item_id);
        }

        if (data.itemQualityType) {
            self.itemQualityType(data.itemQualityType);
        } else {
            var itemTypes = ko.toJS(self.parent().parent().parent().parent().itemTypes);
            _.forEach(itemTypes, i => {
                if (i.id === data.ItemQualityType_id) {
                    self.itemQualityType(i);
                }
            });
        }
        
        self.buy_active(data.buy_active);
        self.sell_active(data.sell_active);
        self.free(data.free);
        self.overridePrice(data.overridePrice);
        self.created_at(moment(data.created_at).format('YYYY-MM-DD'));
        self.updated_at(moment(data.updated_at).format('YYYY-MM-DD'));
        console.log(ko.toJS(self));
    };
    self.save = function () {
        
        try {
            var data = {
                Item_Id: self.item_id(),
                ItemQualityType_Id: self.itemQualityType().id,
                BuyActive: self.buy_active(),
                SellActive: self.sell_active(),
                Free: self.free(),
                OverridePrice: self.overridePrice()
            };
            if (self.id()) {
                data.Id = self.id();
            }

            $.post('ItemQuality/Save', data, function (data, err) {
                console.log(data);
            });
        } catch (e) {
            alert('Please check to ensure its a numeric value');
        }
        
    };
    if (data) {
        self.update(data);
    }
}
