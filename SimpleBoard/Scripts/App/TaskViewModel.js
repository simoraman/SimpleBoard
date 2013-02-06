function taskViewModel() {
    var self = this;

    self.todoTasks = ko.observableArray([]);
    self.doingTasks = ko.observableArray([]);
    self.doneTasks = ko.observableArray([]);

    self.url = "http://localhost:11188/api";

    self.load = function () {
        loadByStatus("ToDo", self.todoTasks);
        loadByStatus("Doing", self.doingTasks);
        loadByStatus("Done", self.doneTasks);
    }
    
    loadByStatus = function (status, list) {

        $.ajax({
            url: self.url + "/tasks/status/"+status,
            dataType: 'json',
            success: function (result) {
                ko.utils.arrayPushAll(list, result);
                list.valueHasMutated();
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
        self.doingTasks.remove(data);
        //        $.ajax({
        //            url: self.url + '/tasks/' + data.Id,
        //            contentType: 'application/json',
        //            type: 'DELETE',
        //            success: function () {
        //                self.load();
        //            },
        //            error: function (request, textStatus, errorThrown) {
        //                alert(textStatus);
        //            }
        //        });
    }
};