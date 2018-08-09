function AdminManagerVm() {
    var self = this;
    self.loaded = ko.observable(false);
    self.authenticated = ko.observable(false);
    self.enteredPassword = ko.observable();
    self.modal = ko.observable();
    self.configs = ko.observableArray();
    self.selectedKey = ko.observable();
    self.keyForEdit = ko.observable();

    self.cancelEdit = function () {
        self.selectedKey().edit(false);
        self.selectedKey(null);
        self.keyForEdit(null);
    };
    self.editKey = function (key) {
        if (self.keyForEdit()) {
            self.cancelEdit();
        }
        self.keyForEdit(new AdminVm(key, self));
        key.edit(true);
        self.selectedKey(key);
        self.selectedKey().edit(true);
        self.keyForEdit().edit(true);
    };
    self.confirmEdit = function (key) {
        $.post('/Admin/Save', {
            Key: key.key(),
            Value: key.value(),
            Id: key.id()
        }, function (data, err) {
            key.edit(false);
            self.cancelEdit();
        });
    }
    self.checkAccess = function () {
        if (!self.enteredPassword()) {
            return;
        }
        $.post('/Admin/CheckAccess', { password: self.enteredPassword() }, function (data, err) {
            if (data) {
                self.authenticated(true);
                self.modal().style.display = "none";
            }
        });
    };
    async.parallel({
        config: loadConfig
    }, function (r,err) {
        r.forEach(c => {
            self.configs.push(new AdminVm(c,self));
        });
        self.loaded(true);
        if (!self.authenticated()) {
            self.modal(document.getElementById('adminModal'));
            self.modal().style.display = "block";
        }
    });

}
function loadConfig(callback) {
    $.get('Admin/All', function (data) {
        callback(data);
    });
};
var am = new AdminManagerVm();
ko.applyBindings(am);