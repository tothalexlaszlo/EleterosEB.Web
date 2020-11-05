var headerViewModel = kendo.observable({
    selectedClient: { fullName: "None Selected" },
    selectedPatient: { name: "None Selected" },
    onPatientDataBound: function (e) {
        var selectedPatient = e.sender.dataItem(e.sender.selectedIndex);
        headerViewModel.set("selectedPatient", selectedPatient);
    }
});

var clientDataSource = new kendo.data.DataSource({
    type: "json",
    transport: {
        read: {
            contentType: "application/json",
            url: "/api/clients"
        }
    }
});

function onSelect(clientItem) {
    var client = this.dataItem(clientItem.item.index());
    headerViewModel.set("selectedClient", client);
    $("#patientSelection").show();
}

function renderPatientTemplate(data) {
    return kendo.Template.compile($('#patient-template').html())(data);
}

function onPatientChange(e) {
    var patient = this.dataItem(e.sender.selectedIndex);
    headerViewModel.set("selectedPatient", patient);
}

$(document).ready(function () {
    kendo.bind($(".client-picker-header"), headerViewModel);
    var myWindow = $("#window").kendoWindow({
        actions: ["Close"],
        height: "300px",
        width: "500px",
        position: {
            top: 200,
            left: 200
        },
        title: "Choose Patient",
        visible: false,
        activate: function () {
            $("#client").select();
        }
    }).data("kendoWindow");

    headerViewModel.set("selectedClient", $.parseJSON(sessionStorage.getItem("client")));
    headerViewModel.set("selectedPatient", $.parseJSON(sessionStorage.getItem("patient")));

    var clientInput = $("#client").kendoAutoComplete({
        select: onSelect,
        minLength: 1,
        filter: "contains",
        placeholder: "Type client name...",
        dataTextField: "fullName",
        dataValueField: "clientId",
        highlightFirst: true,
        template: kendo.template($("#clientpatientpickertemplate").html()),
        dataSource: clientDataSource,
    });

    $(".client-picker").click(function (e) {
        var win = $("#window").data("kendoWindow");
        win.center();
        $("#patientSelection").hide();
        win.open();
        //$("#client").data("kendoAutoComplete").focus();
        $(".client-picker").css("border", "");
    });

    $("#patientDropDown").kendoDropDownList({
        change: onPatientChange
    });

    $("#selectPatientButton").click(function (e) {
        if (typeof (Storage) !== "undefined") {
            sessionStorage["client"] = JSON.stringify(headerViewModel.selectedClient);
            sessionStorage["patient"] = JSON.stringify(headerViewModel.selectedPatient);
        }
        e.preventDefault();
        clientInput.data("kendoAutoComplete").value('');
        clientDataSource.filter({});

        $("#window").data("kendoWindow").close();
    });
});
