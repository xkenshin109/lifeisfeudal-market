function ManagerVm() {
    var self = this;
    self.loaded = ko.observable(false);
    self.itemTypes = ko.observableArray();
    self.categories = ko.observableArray();
    self.loaded = ko.observable(false);

    async.parallel({
        itemTypes: loadItemQualityTypes,
        categories: loadCategories
    }, function (err, r) {
        r.itemTypes.forEach(i => {
            self.itemTypes.push(i);
        });
        r.categories.forEach(c => {
            self.categories.push(new CategoryVm(c, self));
        });
        self.loaded(true);
    });
}

function loadItemQualityTypes(callback) {
    $.get('/ItemQualityType/All', function (data) {
        callback(null, data);
    });
}

function loadCategories(callback) {
    $.get('/Category/All', function (data) {
        callback(null, data);
    });
}

var mv = new ManagerVm();
ko.applyBindings(mv);