function OrderFormVm(data,parent) {
    var self = this;
    self.id = ko.observable();
    self.parent = ko.observable(parent);
    self.name = ko.observable('');
    self.orderNumber = ko.observable('');
    self.products = ko.observableArray();
    self.created_at = ko.observable(moment().format('YYYY-MM-DD'));
    self.updated_at = ko.observable(moment().format('YYYY-MM-DD'));
    self.productForEdit = ko.observable();
    self.selectedProduct = ko.observable();
    self.orderMessage = ko.observable();
    self.deleteProduct = function (product) {
        self.products.remove(product);
    };
    self.editProduct = function (product) {
        if (self.selectedProduct()) {
            self.cancelProduct();
        }
        console.log(ko.toJS(product));
        self.productForEdit(new OrderFormProductsVm(ko.toJS(product), self));
        self.selectedProduct(product);
        self.productForEdit().edit(true);
        self.selectedProduct().edit(true);
    };
    self.cancelEdit = function () {
        if (!self.selectedProduct().confirmed()) {
            self.products.remove(self.selectedProduct());
        }
        self.productForEdit().edit(false);
        self.selectedProduct().edit(false);
        self.productForEdit(null);
        self.selectedProduct(null);
    };
    self.confirmEdit = function (quality) {
        quality.update(ko.toJS(self.productForEdit()));
        self.selectedProduct().update(ko.toJS(self.productForEdit()));
        self.selectedProduct().edit(false);
        self.selectedProduct(null);
        self.productForEdit(null);
    };
    self.update = function (data) {
        self.name(data.PlayerName);
        self.id(data.Id);
        self.orderNumber(data.OrderNumber);
        self.created_at(moment(data.Created).format('YYYY-MM-DD'));
        self.updated_at(moment(data.Updated).format('YYYY-MM-DD'));
    };
    self.enableSave = ko.computed(function(){
        if (self.products().length > 0 && self.name() !== undefined) {
            return true;
        }
        return false;
    });
    self.addItem = function () {
        if (self.selectedProduct()) {
            self.cancelEdit();
        }
        var x = new OrderFormProductsVm(null, self);
        x.edit(true);
        self.products.push(x);
        self.editProduct(x);
    };
    self.validations = function () {
        var ret = [];
        if (self.name() === undefined || self.name() === "") {
            ret.push('Please provide a name before continuing');
        }
        if (self.products().length === 0) {
            ret.push('No items in cart');
        }
        return ret;
    };
    self.save = function () {
        console.log(self.validations());
        if (self.validations().length === 0) {
            var x = {
                PlayerName: self.name(),
                Products: []
            };
            self.products().forEach(p => {
                var r = {
                    ItemQuality_Id: p.getQuality().Id,
                    Selling: p.isSelling(),
                    Price: p.price(),
                    Quantity: p.quantity()
                };
                x.Products.push(r);
            });
            if (self.id()) {
                x.Id = self.id();
            }
            $.post('/OrderForm/Save',{ product: x }, function (d) {
                self.update(d);
                return self.send();
            });
        } else {
            alert(self.validations().join('\n'));
        }
        
    };

    self.send = function () {
        var order = {
            PlayerName: self.name(),
            OrderNumber: self.orderNumber()
        };
        var prod = [];
        console.log(ko.toJS(self.products()));
        _.forEach(self.products(), (p) => {
            var s = this;
            var itemQuality = _.find(self.parent().allItemQuality(), (i) => {
                return i.Item_Id === p.item().id && i.ItemQualityType_Id === p.itemQualityType().id;
            });
            p.orderForm_id(self.id());
            p.itemQuality(itemQuality);
            p.updatePrice();
            prod.push(p);
        });
        var message = '';
        var selling = _.filter(prod, (p) => {
            console.log(ko.toJS(p));
            return p.Selling;
        });
        if (selling.length > 0) {
            message += 'Selling: \n';
            _.forEach(selling, (p) => {
                //p.save();
                message += p.Quantity + ' X ' + p.item().name + '(' + p.itemQualityType().name + ') ' + p.item().price + ' = ' + p.price() + '\n';
            });
            var total = _.sum(selling, function (s) { return s.price(); });
            console.log(total);
        }
        var buying = _.filter(prod, (p) => { return !p.Selling; });
        if (buying.length > 0) {
            message += '\n Buying:\n';
            _.forEach(buying, (p) => {
                console.log(ko.toJS(p));
                //p.save();
                message += p.quantity() + ' X ' + p.item().name + '(' + p.itemQualityType().name + ') ' + (p.item().price ? p.item().price : 0) + ' = ' + (p.price() ? p.price() : 0) + '\n';
            });
        }
        var d = _.map(prod, (p) => {
            return {
                OrderForm_id: p.orderForm_id(),
                ItemQuality_id: p.itemQuality().Id,
                Selling: p.isSelling(),
                Price: p.price(),
                Quantity: p.quantity()
            };
        });
        var cd = {
            Id: self.id(),
            PlayerName: self.name(),
            OrderNumber: self.orderNumber()
        };
        $.post('OrderForm/SendOrder', {order:cd,products:d}, function (data, err) {
        });
        self.orderMessage(message);
    };
    if (data) {
        self.update(data);
    }

}
