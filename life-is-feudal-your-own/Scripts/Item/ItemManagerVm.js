function ManagerVm() {
    var self = this;
    self.edit = ko.observable(false);
    self.loaded = ko.observable(false);
    self.items = ko.observableArray();
    self.itemTypes = ko.observableArray();
    self.itemQualities = ko.observableArray();
    self.categories = ko.observableArray();
    async.parallel({
        //items: loadItems,
        itemTypes: loadItemQualityTypes,
        //itemQuality: loadItemQualities,
        categories: loadCategories,
        subcategories: loadSubCategories
    }, function (err, r) {
        r.itemTypes.forEach(i => {
            self.itemTypes.push(i);
        });
        //r.itemQuality.forEach(i => {
        //    self.itemQualities.push(i);
        //});
        //r.itemTypes.forEach(i => {
        //    self.itemTypes.push(i);
        //});
        //r.categories.forEach(c => {
        //    var citems = _.filter(r.items, (i) => {
        //        return c.id === i.category_id;
        //    });
        //    var a = _.map(citems, function (ci) {
        //        return ci.sub_category;
        //    });
        //    a= _.uniq(a);
        //    var c1 = new CategoryVm(c, self);
        //    var subs = _.filter(r.subcategories, function (sc) {
        //        return _.some(a, x => { return x === sc.id; });
        //    });
        //    c1.addSubCategories(subs, citems);
        //    self.categories.push(c1);
        //});
    });
}
function loadItems(callback) {
    $.get("/Item/All", function (data) {
        callback(null, data);
    });
}
function loadItemQualityTypes(callback) {
    $.get('/ItemQualityType/All', function (data) {
        callback(null, data);
    });
}
function loadItemQualities(callback) {
    $.get('/ItemQuality/All', function (data) {
        callback(null, data);
    });
}
function loadCategories(callback) {
    $.get('/Category/All', function (data) {
        callback(null, data);
    });
}
function loadSubCategories(callback) {
    $.get('/SubCategory/All', function (data) {
        callback(null, data);
    });
}
var mv = new ManagerVm();
ko.applyBindings(mv);