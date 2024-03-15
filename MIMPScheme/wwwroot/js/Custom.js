$(document).ready(function () {
    BindState();
    ShowUserData();

});

function HideModifyForm()
{
    document.getElementById("footermodifyBtn").style.display = "none";
    document.getElementById("MainTable").style.display = "block";
    document.getElementById("footerbtnModify").display = "none";
    document.getElementById("AddUsertbl").style.display = "none";
}
function ShowAddUser()
{
    document.getElementById("MainTable").style.display = "none";
    document.getElementById("AddUsertbl").style.display = "inline-table";
    document.getElementById("footerbtn").style.display = "block";
    clear();
}
var userIdvalue = "";
var disctricvalue = "";
function EditAddUser(username, statevalue, districtname, name1, email1, des1, name2, email2, des2, name3, email3, des3, userId)
{
    userIdvalue = userId;
    disctricvalue = districtname;
    document.getElementById("MainTable").style.display = "none";
    document.getElementById("AddUsertbl").style.display = "inline-table";
    document.getElementById("footermodifyBtn").style.display = "block";
    document.getElementById("footermodifyBtn").style.display = "block"; 
    document.getElementById("txtUserName1").value = username;
    document.getElementById("txtname1").value = name1;
    document.getElementById("txtemail1").value = email1;
    document.getElementById("txtdesignation1").value = des1;
    document.getElementById("txtname2").value = name2;
    document.getElementById("txtemail2").value = email2;
    document.getElementById("txtdesignation2").value = des2;
    document.getElementById("txtname3").value = name3;
    document.getElementById("txtemail3").value = email3;
    document.getElementById("txtdesignation3").value = des3;
    var dd = document.getElementById('state1');
    for (var i = 0; i < dd.options.length; i++) {
        if (dd.options[i].value === statevalue) {
            dd.selectedIndex = i;
            BindDistrict(statevalue);
            break;
        }
    }
    BindDistrictWithSelectedItem(statevalue);
}
function BindDistrictWithSelectedItem(statevalue) {
    var statevalue = {
        userId: statevalue
    };
    $.ajax({
        url: '/User/BindDistrict1',
        type: 'Get',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8;',
        data: statevalue,
        success: function (result, status, xhr) {
            var elementId = '#disCityMultiSelect1';
            var sourcedata = '<select id="distric1" name="distric1" multiple="multiple">';
            var array = disctricvalue.split(",");
            debugger;
            for (var i = 0; i < result.length; i++)
            {
                if (result[i].districName != null)
                {
                    for (var j = 0; j < array.length; j++)
                    {
                        if (result[i].districName == array[j])
                        {
                            sourcedata += '<option value=' + result[i].userId + ' selected>' + "  " + result[i].districName + '</option>';
                           
                        }
                    }
                }
                else {
                    sourcedata += '<option value=' + result[i].userId + '>' + "  " + result[i].districName + '</option>';

                }
            }
            sourcedata += '</select>';
            $(elementId).html(sourcedata);
            $("select#distric1")
                .multiselect();
        },
        error: function () {
            alert("Add user detail not saved");
        }
    });
}

function modifyUserForm1()
{
    var txtUserName = $("#txtUserName1").val();
    var txtPassword = $("#txtPassword1").val(); 
    var name1 = $("#txtname1").val();
    var email1 = $("#txtemail1").val(); 
    var desi1 = $("#txtdesignation1").val();
    var name2 = $("#txtname2").val();
    var email2 = $("#txtemail2").val();
    var desi2 = $("#txtdesignation2").val();
    var name3 = $("#txtname3").val();
    var email3 = $("#txtemail3").val();
    var desi3 = $("#txtdesignation3").val();
    var dropdown1 = document.getElementById("state1");
    var selectedOption1 = dropdown1.options[dropdown1.selectedIndex];
    var txt1 = selectedOption1.textContent;
    var stateId = selectedOption1.value;
    const chBoxes = document.querySelectorAll('.ui-multiselect-checkboxes input[type="checkbox"]:checked');
    let inputchkVal = [];
    chBoxes.forEach(function (cb) {
        inputchkVal.push(cb.value);
    });
    
    if (name1 == "") {
        alert("Please Enter First Name");
        return false;
    }
    if (email1 == "") {
        alert("Please Enter First Email");
        return false;
    }
    if (desi1 == "") {
        alert("Please Enter First Designation");
        return false;
    }

    if (name2 == "") {
        alert("Please Enter Second Name");
        return false;
    }
    if (email2 == "") {
        alert("Please Enter Second Email");
        return false;
    }
    if (desi2 == "") {
        alert("Please Enter Second Designation");
        return false;
    }

    if (name3 == "") {
        alert("Please Enter Second Name");
        return false;
    }
    if (email3 == "") {
        alert("Please Enter Second Email");
        return false;
    }
    if (desi3 == "") {
        alert("Please Enter Second Designation");
        return false;
    }
    if (stateId < 1) {
        alert("Please Select State");
        return false;
    }
    if (inputchkVal == "") {
        alert("Please Select District");
        return false;
    }
    if (txtUserName == "") {
        alert("Please Enter Username");
        return false;
    }
    if (txtPassword == "") {
        alert("Please Enter Password");
        return false;
    }
    var addUser = {
        stateId: stateId,
        districId: inputchkVal,
        username: txtUserName,
        password: txtPassword,
        Fname: name1,
        user_idd: userIdvalue,
        Femail: email1,
        Fdesignation: desi1,
        Sname: name2,
        Semail: email2,
        Sdesignation: desi2,
        Tname: name3,
        Temail: email3,
        Tdesignation: desi3
    };
    $.ajax({
        url: '/User/AddModifyUserDetail',
        type: 'POST',
        data: addUser,
        success: function (result, status, xhr) {
            if (result == true) {
                document.getElementById("errorMsg1").style.display = "contents";
                clear();
            }
            else {
                location.reload();
            }
        },
        error: function () {
            alert("Add user detail not saved");
        }
    });
}


function statusCheck(status_id, userid)
{

    var addStatus = {
        statusid: status_id,
        userId: userid
    }
    $.ajax({
        url: '/User/UserStatus',
        type: 'POST',
        data: addStatus,
        success: function (result, status, xhr) {
            location.reload();
        },
        error: function () {
            alert("Status not changed");
        }
    });

}
function ShowListHeader()
{
    document.getElementById("ShowAddStateUser").style.display = "none";
    document.getElementById("HidelistTableHeader").style.display = "contents";
}
function addStateUser()
{
    document.getElementById("ShowAddStateUser").style.display = "table-row";
    document.getElementById("HidelistTableHeader").style.display = "none";
    document.getElementById("footermodifyBtn").style.display = "none";
    document.getElementById("errorMsg").style.display = "none";
    document.getElementById("state").selectedIndex = 0;
 
}
function addStateUserForm()
{
    var dropdown = document.getElementById("state");
    var selectedOption = dropdown.options[dropdown.selectedIndex];
    var txt = selectedOption.textContent;
    var value = selectedOption.value;
   
    if (value > 0) {
        var addState = {
            username: $("#txtUserName").val(),
            password: $("#txtPassword").val(),
            stateName: txt,
            stateValue: value
        }
        $.ajax({
            url: '/User/AddStateUser',
            type: 'POST',
            data: addState,
            success: function (result, status, xhr) {
                if (result == true)
                {
                    document.getElementById("errorMsg").style.display = "contents";
                }
                else {
                    ShowListHeader();
                }
            },
            error: function () {
                alert("Add StateAdd User can not be Save");
            }
        });
    }
    else {
        alert('Please select state');
        return false;
    }


}

function clear()
{
    document.getElementById("txtUserName1").value = "";
    document.getElementById("txtPassword1").value = "";
    document.getElementById("txtname1").value = "";
    document.getElementById("txtemail1").value = "";
    document.getElementById("txtdesignation1").value = "";
    document.getElementById("txtname2").value = "";
    document.getElementById("txtemail2").value = "";
    document.getElementById("txtdesignation2").value = "";
    document.getElementById("txtname3").value = "";
    document.getElementById("txtemail3").value = "";
    document.getElementById("txtdesignation3").value = "";
    
    document.getElementById("distric1").selectedIndex = "0";
    document.getElementById("state1").selectedIndex = "0";

    var elementId = '#disCityMultiSelect1';
    var sourcedata = '<select id="distric1" name="distric1" multiple="multiple">';
    sourcedata += '</select>';
    $(elementId).html(sourcedata);
    $("select#distric1")
        .filter(".single")
        .multiselect({
            multiple: false,
            noneSelectedText: 'Please select a radio',
            header: false
        })
        .end()
        .not(".single")
        .multiselect({
            noneSelectedText: 'Please select a checkbox'
        });
}
function BindState() {
    $.ajax({
        url: '/User/AddState',
        type: 'Get',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8;',
        success: function (result, status, xhr) {
            debugger;
            var sourcedata = '<option value="0">Select State</option>';
            $.each(result, function (index, item) {
                sourcedata += '<option value=' + item.id + '>' + item.stateName + '</option>';
            });
            $("#dropdownState select").html(sourcedata);
            $("#dropdownState1 select").html(sourcedata);
        },
        error: function () {
            alert("StateAdd can not be bind");
        }
    });
}


function selectstate()
{
    var selectElement = document.querySelector('#state1');
    var stateId = selectElement.value;
    BindDistrict(stateId);
}

function BindDistrict(stateId)
{
    var statevalue = {
        userId: stateId
    };
    $.ajax({
        url: '/User/BindDistrict',
        type: 'Get',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8;',
        data: statevalue,
        success: function (result, status, xhr) {
            debugger;
            var elementId = '#disCityMultiSelect1';
            var sourcedata = '<select id="distric1" name="distric1" multiple="multiple">';
            $.each(result, function (index, item) {
                sourcedata += '<option value=' + item.districId + '>' + "  " + item.districName + '</option>';

            });
            sourcedata += '</select>';
            $(elementId).html(sourcedata);
            $("select#distric1")
              .multiselect();
        },
        error: function () {
            alert("District Add can not be bind");
        }
    });
}
function ShowListHeaderM()
{
    
    document.getElementById("ShowAddStateUser").style.display = "none";
    document.getElementById("footerbtn").style.display = "none";
    document.getElementById("AddUsertbl").style.display = "none";
    document.getElementById("MainTable").style.display = "block";
    document.getElementById("footerbtnModify").display = "none";
}

function checkAddUserForm()
{
    var selectElement = document.querySelector('#state1');
    var stateId = selectElement.value;
    const chBoxes = document.querySelectorAll('.ui-multiselect-checkboxes input[type="checkbox"]:checked');
    let inputchkVal = [];
    chBoxes.forEach(function (cb) {
        inputchkVal.push(cb.value);
    });
    var txtUserName = $("#txtUserName1").val();
    var txtPassword = $("#txtPassword1").val();
    var name1 = $("#txtname1").val();
    var Email1 = $("#txtemail1").val();
    var desi1 = $("#txtdesignation1").val();
    var name2 = $("#txtname2").val();
    var Email2 = $("#txtemail2").val();
    var desi2 = $("#txtdesignation2").val();
    var name3 = $("#txtname3").val();
    var Email3 = $("#txtemail3").val();
    var desi3 = $("#txtdesignation3").val();

    if (name1 == "") {
        alert("Please Enter First Name");
        return false;
    }
    if (Email1 == "") {
        alert("Please Enter First Email");
        return false;
    }
    if (desi1 == "") {
        alert("Please Enter First Designation");
        return false;
    }

    if (name2 == "") {
        alert("Please Enter Second Name");
        return false;
    }
    if (Email2 == "") {
        alert("Please Enter Second Email");
        return false;
    }
    if (desi2 == "") {
        alert("Please Enter Second Designation");
        return false;
    }

    if (name3 == "") {
        alert("Please Enter Second Name");
        return false;
    }
    if (Email3 == "") {
        alert("Please Enter Second Email");
        return false;
    }
    if (desi3 == "") {
        alert("Please Enter Second Designation");
        return false;
    }
    if (stateId == "") {
        alert("Please Select State");
        return false;
    }
    if (chBoxes == "") {
        alert("Please Select District");
        return false;
    }
    if (txtUserName == "") {
        alert("Please Enter Username");
        return false;
    }
    if (txtPassword == "") {
        alert("Please Enter Password");
        return false;
    }
    var addUser = {
        stateId: stateId,
        districId: inputchkVal,
        username: txtUserName,
        password: txtPassword,
        Fname: name1,
        Femail: Email1,
        Fdesignation: desi1,
        Sname: name2,
        Semail: Email2,
        Sdesignation: desi2,
        Tname: name3,
        Temail: Email3,
        Tdesignation: desi3
    };
    $.ajax({
        url: '/User/AddUserDetail',
        type: 'POST',
        data: addUser,
        success: function (result, status, xhr) {
            if (result == true)
            {
                document.getElementById("errorMsg1").style.display = "contents";
                var selectElement = document.querySelector('#state1');
                var stateId = selectElement.value;
                var selectElement1 = document.querySelector('#distric');
                var districId = selectElement1.value;
                clear();
                
            }
            else {
                location.reload();
            }
        },
        error: function () {
            alert("Add user detail not saved");
        }
    });

}

function ShowUserData() {
    $.ajax({
        url: '/User/UserList',
        type: 'Get',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8;',
        success: function (result, status, xhr) {
            array = result;
            displayIndexButtons();
        },
        error: function () {
            alert("Data can not be get");
        }

    });
}

var array = [];
var array_length = 0;
var table_size = 10;
var start_index = 1;
var end_index = 0;
var current_index = 1;
var max_index = 0;

function preLoadCalculations() {
    array_length = array.length;
    max_index = array_length / table_size;

    if ((array_length % table_size) > 0) {
        max_index++;
    }
}

function displayIndexButtons() {
    preLoadCalculations();
    $(".index_buttons button").remove();
    $(".index_buttons").append('<button onclick="prev();">Previous</button>');
    for (var i = 1; i <= max_index; i++) {
        $(".index_buttons").append('<button onclick="indexPagination(' + i + ');" index="' + i + '">' + i + '</button>');
    }
    $(".index_buttons").append('<button onclick="next();">Next</button>');
    highlightIndexButton();
}

function highlightIndexButton() {
    start_index = ((current_index - 1) * table_size) + 1;
    end_index = (start_index + table_size) - 1;
    if (end_index > array_length) {
        end_index = array_length;
    }
    $(".footer span").text('Showing ' + start_index + ' to ' + end_index + ' of ' + array_length + ' entries');
    $(".index_buttons button").removeClass("active");
    $(".index_buttons button[index='" + current_index + "']").addClass("active");
    displayTableRows();
}

function displayTableRows() {
    
    $(".table table tbody tr").remove();
    var tab_start = start_index - 1;
    var tab_end = end_index;
    for (var i = tab_start; i < tab_end; i++) {
        var result = array[i];
            var row1 = '';
            if (i % 2) {
                row1 += '<tr class="row1">';
                row1 += '<td style="text-align:center;word-break:break-all;">' + result['userName'] + '</td>';
                row1 += '<td style="text-align:center;word-break:break-all;">' + result['stateName'] + '</td>';
                row1 += '<td style="text-align:center;word-break:break-all;">' + result['districtName'] + '</td>';
                row1 += '<td style="text-align:center;">' + result['lastLogin'] + '</td>';
                if (result['userId'] != '1') {
                    if (result['status'] == '1') {
                        if (result['user_level'] == '0') {
                            row1 += '<td style="text-align:center;"><a href="javascript:statusCheck(0,' + result['userId'] + ');">Active</a>&nbsp;/&nbsp;Deactive' + ' ' + '<a href = "javascript:EditAddUser(`' + result['userName'] + '`,`' + result['statevalue'] + '`,`' + result['districtName'] + '`,`' + result['name1'] + '`,`' + result['email1'] + '`,`' + result['desgination1'] + '`,`' + result['name2'] + '`,`' + result['email2'] + '`,`' + result['desgination2'] + '`,`' + result['name3'] + '`,`' + result['email3'] + '`,`' + result['desgination3'] + '`,`' + result['userId'] + '`);">Edit</a>' + '</td>';
                        }
                        else {
                            row1 += '<td style="text-align:center;"><a href="javascript:statusCheck(0,' + result['userId'] + ');">Active</a>&nbsp;/&nbsp;Deactive' + '</td>';
                        }
                    }
                    else {
                        if (result['user_level'] == '0') {
                            row1 += '<td style="text-align:center;"><a href="javascript:statusCheck(0,' + result['userId'] + ');">Active</a>&nbsp;/&nbsp;Deactive' + ' ' + '<a href = "javascript:EditAddUser(`' + result['userName'] + '`,`' + result['statevalue'] + '`,`' + result['districtName'] + '`,`' + result['name1'] + '`,`' + result['email1'] + '`,`' + result['desgination1'] + '`,`' + result['name2'] + '`,`' + result['email2'] + '`,`' + result['desgination2'] + '`,`' + result['name3'] + '`,`' + result['email3'] + '`,`' + result['desgination3'] + '`,`' + result['userId'] + '`);">Edit</a>' + '</td>';

                        }
                        else {
                            row1 += '<td style="text-align:center;"><a href="javascript:statusCheck(1,' + result['userId'] + ');">Deactive</a> &nbsp;/&nbsp;Active' + '</td>';

                        }

                    }
                }
                row1 += '</tr>';
            }
            else {
                row1 += '<tr class="row2">';
                row1 += '<td style="text-align:center;word-break:break-all;">' + result['userName'] + '</td>';
                row1 += '<td style="text-align:center;word-break:break-all;">' + result['stateName'] + '</td>';
                row1 += '<td style="text-align:center;word-break:break-all;">' + result['districtName'] + '</td>';
                row1 += '<td style="text-align:center;">' + result['lastLogin'] + '</td>';

                if (result['userId'] != '1') {
                    if (result['status'] == '1') {
                        if (result['user_level'] == '0') {
                            row1 += '<td style="text-align:center;"><a href="javascript:statusCheck(0,' + result['userId'] + ');">Active</a>&nbsp;/&nbsp;Deactive' + ' ' + '<a href = "javascript:EditAddUser(`' + result['userName'] + '`,`' + result['statevalue'] + '`,`' + result['districtName'] + '`,`' + result['name1'] + '`,`' + result['email1'] + '`,`' + result['desgination1'] + '`,`' + result['name2'] + '`,`' + result['email2'] + '`,`' + result['desgination2'] + '`,`' + result['name3'] + '`,`' + result['email3'] + '`,`' + result['desgination3'] + '`,`' + result['userId'] + '`);">Edit</a>' + '</td>';

                        }
                        else {
                            row1 += '<td style="text-align:center;"><a href="javascript:statusCheck(0,' + result['userId'] + ');">Active</a>&nbsp;/&nbsp;Deactive' + '</td>';
                        }
                    }
                    else {
                        if (result['user_level'] == '0') {
                            row1 += '<td style="text-align:center;"><a href="javascript:statusCheck(0,' + result['userId'] + ');">Active</a>&nbsp;/&nbsp;Deactive' + ' ' + '<a href = "javascript:EditAddUser(`' + result['userName'] + '`,`' + result['statevalue'] + '`,`' + result['districtName'] + '`,`' + result['name1'] + '`,`' + result['email1'] + '`,`' + result['desgination1'] + '`,`' + result['name2'] + '`,`' + result['email2'] + '`,`' + result['desgination2'] + '`,`' + result['name3'] + '`,`' + result['email3'] + '`,`' + result['desgination3'] + '`,`' + result['userId'] + '`);">Edit</a>' + '</td>';

                        }
                        else {
                            row1 += '<td style="text-align:center;"><a href="javascript:statusCheck(1,' + result['userId'] + ');">Deactive</a> &nbsp;/&nbsp;Active' + '</td>';

                        }

                    }
                }

                row1 += '</tr>';
            }
            $('#tblUserdata').append(row1);
    }
}

function next() {
    debugger;
    if (current_index < max_index) {
        current_index++;
        highlightIndexButton();
    }
}

function prev() {

    if (current_index > 1) {
        current_index--;
        highlightIndexButton();
    }
}

function indexPagination(index) {
    current_index = parseInt(index);
    highlightIndexButton();
}