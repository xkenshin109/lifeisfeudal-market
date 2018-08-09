function SubCategoryVm(data, parent) {
    var self = this;
    self.parent = ko.observable(parent);
    self.id = ko.observable(0);
    self.name = ko.observable('');
    self.items = ko.observableArray();
    self.expanded = ko.observable(false);
    self.itemForEdit = ko.observable(null);
    self.selectedItem = ko.observable(null);
    
    self.editItem = function (item) {
        if (self.selectedItem()) {
            self.cancelEdit();
        }
        self.itemForEdit(new ItemVm(ko.toJS(item), self));
        self.selectedItem(item);
        self.selectedItem().edit(true);
        self.itemForEdit().edit(true);
    };
    self.cancelEdit = function () {
        self.itemForEdit().edit(false);
        self.selectedItem().edit(false);
        if (!self.selectedItem().id()) {
            self.items.remove(self.selectedItem());
        }
        self.selectedItem(null);
        self.itemForEdit(null);
    };
    self.confirmEdit = function () {
        self.selectedItem().update(ko.toJS(self.itemForEdit()));
        self.selectedItem().edit(false);
        self.selectedItem().save();
        self.selectedItem(null);
        self.itemForEdit(null);
    };
    self.expand = function () {
        self.expanded(!self.expanded());
        $.get('/Item/GetItemsById', { catId:self.parent().id,subId: self.id() }, function (data,err)
        {
            self.items.removeAll();
            data.forEach(i => {
                self.items.push(new ItemVm(i,self));
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