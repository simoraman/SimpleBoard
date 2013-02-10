function tasksViewModel() {
    var self = this;

    self.tasks = ko.observableArray([]);

    self.url = "http://localhost:11188/api";

    self.load = function () {
        $.ajax({
            url: self.url + "/tasks/",
            dataType: 'json',
            success: function (result) {
                ko.utils.arrayPushAll(self.tasks, result);
                self.tasks.valueHasMutated();
            },
            error: function (request, textStatus, errorThrown) {
                alert(textStatus);
            }
        });
    }
    
    self.updateItem = function (formData) {
        var putData = ko.mapping.toJSON(formData)
        $.ajax({
            url: self.url + '/tasks/' + formData.Id,
            contentType: 'application/json',
            type: 'PUT',
            data: putData,
            error: function (request, textStatus, errorThrown) {
                alert(textStatus);
            }
        });
    }
    self.Delete = function (data) {
        self.tasks.remove(data);
        $.ajax({
            url: self.url + '/tasks/' + data.Id,
            contentType: 'application/json',
            type: 'DELETE',
            success: function () {
            },
            error: function (request, textStatus, errorThrown) {
                alert(textStatus);
            }
        });
    }

//    self.newTask = function (data) {
//        var v = data;
//        self.tasks.push(v);
//        self.tasks.valueHasMutated();
//    }
};