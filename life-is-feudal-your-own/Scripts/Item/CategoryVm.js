function CategoryVm(data, parent) {
    var self = this;
    self.parent = ko.observable(parent);
    self.id = ko.observable(0);
    self.name = ko.observable('');
    self.subcategories = ko.observableArray();
    self.expanded = ko.observable(false);

    self.expand = function () {
        self.expanded(!self.expanded());
        self.subcategories.removeAll();
        $.get('/Category/GetSubCategoriesById', { id: self.id() }, function (data,err) {
            data.forEach(sc => {
                self.subcategories.push(new SubCategoryVm(sc,self));
            });
        });
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