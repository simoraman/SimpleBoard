function tasksViewModel() {
    var self = this;

    self.tasks = ko.observableArray([]);

    self.selectedTask = ko.observable({Description: "", Status:"ToDo"});

    self.setSelectedTask = function (item) {
        self.selectedTask(item);
    }

    self.load = function () {
        $.ajax({
            url: backendUrl + "/tasks/",
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
            url: backendUrl + '/tasks/' + formData.Id,
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
            url: backendUrl + '/tasks/' + data.Id,
            contentType: 'application/json',
            type: 'DELETE',
            success: function () {
            },
            error: function (request, textStatus, errorThrown) {
                alert(textStatus);
            }
        });
    }

    self.create = function (item) {
        var postData = ko.mapping.toJSON(item)

        $.ajax({
            url: backendUrl + '/tasks/',
            contentType: 'application/json',
            type: 'POST',
            data: postData,
            success: function () {
            },
            error: function (request, textStatus, errorThrown) {
                alert(textStatus);
            }
        });
    }

};