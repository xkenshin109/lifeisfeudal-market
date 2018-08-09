function AdminVm(data,parent) {
    var self = this;
    self.parent = ko.observable(parent);
    self.id = ko.observable();
    self.key = ko.observable();
    self.value = ko.observable();
    self.edit = ko.observable(false);
    self.update = function (data) {
        self.parent();
        self.id(data.id);
        self.key(data.key);
        self.value(data.value);
    };
    if (data) {
        self.update(data);
    }
}