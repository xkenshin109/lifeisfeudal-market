function CategoryVm(data, parent) {
    let self = this;
    self.parent = ko.observable(parent);
    self.id = ko.observable(0);
    self.name = ko.observable('');
    self.subcategories = ko.observableArray();
    self.expanded = ko.observable(false);

    self.addSubCategories = function (subcats, items) {
        _.forEach(subcats, (sc) => {
            var scitems = _.filter(items, (i) => { return i.sub_category === sc.id; });
            var scnew = new SubCategoryVm(sc, self);
            scnew.addItems(scitems);
            self.subcategories().push(scnew);            
        });
    };
    self.expand = function () {
        self.expanded(!self.expanded());
    };
    self.update = function (data) {
        self.id(data.id);
        self.name(data.name);
        self.expanded(false);
    };
    if (data) {
        self.update(data);
    }
}