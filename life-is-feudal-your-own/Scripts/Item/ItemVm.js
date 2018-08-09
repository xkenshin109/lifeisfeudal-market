function ItemVm(data, parent) {
    var self = this;

    self.parent = ko.observable(parent);
    self.expand = ko.observable(false);
    self.edit = ko.observable(false);
    self.id = ko.observable(0);
    self.price = ko.observable(0);
    self.name = ko.observable('');
    //self.created_at = ko.observable('');
    //self.updated_at = ko.observable('');

    self.quality = ko.observableArray();
    self.qualityForEdit = ko.observable();
    self.selectedQuality = ko.observable();

    self.editQuality = function (quality) {
        console.log(ko.toJS(quality));
        if (self.selectedQuality()) {
            self.cancelEdit();
        }
        
        self.qualityForEdit(new ItemQualityVm(ko.toJS(quality), self));
        self.selectedQuality(quality);
        
        self.selectedQuality().edit(true);
        self.qualityForEdit().edit(true);
    };
    self.cancelEdit = function () {
        self.selectedQuality().edit(false);
        if (self.qualityForEdit().id() === null) {
            self.quality.remove(self.selectedQuality());
        }
        self.qualityForEdit(null);
        self.selectedQuality(null);
    };
    self.confirmEdit = function (quality) {

        self.selectedQuality().update(ko.toJS(self.qualityForEdit()));
        self.selectedQuality().edit(false);
        self.selectedQuality().save();
        self.selectedQuality(null);
        self.qualityForEdit(null);
    };
    self.addNewQuality = function () {
        if (self.selectedQuality()) {
            self.cancelEdit();
        }
        var x = new ItemQualityVm(null, self);
        x.edit(true);
        self.quality.push(x);
        self.editQuality(x);
    };
    self.expanded = function () {
        self.expand(!self.expand());
        if (self.expand()) {
            $.get('/Item/GetItemQualitiesById', { id: data.id }, function (data, err) {
                self.quality.removeAll();
                if (err) { console.log(err); }
                data.forEach(d => {
                    self.quality.push(new ItemQualityVm(d, self));
                });
            });    
        }        
    };
    self.update = function (data) {
        self.id(data.id);
        self.name(data.name);
        self.price(data.price);
        //self.created_at(moment(data.created_at).format('YYYY-MM-DD'));
        //self.updated_at(moment(data.updated_at).format('YYYY-MM-DD'));
        
    };
    self.save = function () {
        var self = this;
        var data = {
            Name: self.name(),
            Price: self.price()?parseInt(self.price()):0
            //created_at: self.created_at(),
            //updated_at: self.updated_at()
        };
        if (self.id()) {
            data.id = self.id();
        }

        $.post('Item/SaveItem', data, function (err, data) {
            console.log(err);
            console.log(data);
        });
    };
    if (data) {
        self.update(data);
    }
}
