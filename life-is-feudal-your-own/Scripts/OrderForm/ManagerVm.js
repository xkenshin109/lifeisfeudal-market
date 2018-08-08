var allItems = [];
var allItemQuality = [];

function ManagerVm() {
    var self = this;
    self.loaded = ko.observable(false);
    self.items = ko.observableArray();
    self.allItems = ko.observableArray();
    self.allItemQuality = ko.observableArray();
    self.itemTypes = ko.observableArray();
    self.orderForm = ko.observable(new OrderFormVm(null,self));
    async.parallel({
        items: loadItems,
        itemQuality: loadItemQuality,
        itemTypes: loadItemQualityTypes
    }, function (err, r) {
        self.allItems = r.items;
        allItems = r.items;
        self.allItemQuality = r.itemQuality;
        allItemQuality = r.itemQuality;
        self.itemTypes = r.itemTypes;
    });
}
function loadItems(callback) {
    $.get("/Item/All", function (data) {
        callback(null, data);
    });
}
function loadItemQuality(callback) {
    $.get("/ItemQuality/All", function (data) {
        callback(null, data);
    });
}
function loadItemQualityTypes(callback) {
    $.get('/ItemQualityType/All', function (data) {
        callback(null, data);
    });
}
function loadSubCategories(callback) {
    $.get('/SubCategories/All', function (data) {
        callback(null, data);
    });
}
function loadCategories(callback) {
    $.get('/Categories/All', function (data) {
        callback(null, data);
    });
}
var mv = new ManagerVm();
ko.applyBindings(mv);