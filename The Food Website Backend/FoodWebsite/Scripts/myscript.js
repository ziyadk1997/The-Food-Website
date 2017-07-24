var broadcastid;
var restaurantid;

function LoadRestaurants(restList) {
    $('#orderSetupItems li:not(:first)').remove();
    $("#orderSetupItems").append("<li class=\"restButton list-group-item active\" restId = "  + restList[restList.length - 1].Name + "</li>");
    for (var i = restList.length - 2; i >=0 ; i--) {
        var name = restList[i].Name;
        $("#orderSetupItems").append("<li class=\"restButton list-group-item\" restId = " +  name + "</li>");
    }


}
function LoadOrders(orders) {
    $('#user_order_details').slice(1).remove();
    for (var i = 0; i < 1; i++) {
        var name = "McPizza";
            //orders[i].Item.Name;
        var quantity = 2;
            //orders[i].Quantity;
        var comments = "Stuft Crust";
            //orders[i].comments;
        $("#user_order_details").append("<tr> <td>"+name + "</td> <td>" + quantity + "</td><td>" + comments+ "</th><td>Delete</td></tr>");
    }
}

function LoadRestaurantItems(items) {
    $('#ItemSetupItems li:not(:first)').remove();
    $("#ItemSetupItems").append("<li class=\"restButton list-group-item active\" restId = " + items[items.length - 1].RestaurantID + ">" + items[items.length - 1].Name + "</li>");
    for (var i = items.length - 2; i >= 0 ; i--) {
        var name = items[i].Name;
        $("#ItemSetupItems").append("<li class=\"restButton list-group-item\" restId = " + items[i].RestaurantID + ">" + name + "</li>");
    }
}


function initializeOrderList()
{
    var input, filter, ul, li, a, i;
    input = document.getElementById("orderSetupFilterList");
    filter = input.value.toUpperCase();
    ul = document.getElementById("orderSetupItems");
    ul.style.display = "";
    li = ul.getElementsByTagName('li');

    for (i = 0; i < li.length; i++) {
        a = li[i];
        if (a == null || a.innerHTML.toUpperCase().indexOf(filter) > -1) {
            li[i].style.display = "";
        } else {
            li[i].style.display = "none";
        }
    }

}

function initializeItemList() {
    var input, filter, ul, li, a, i;
    input = document.getElementById("ItemSetupFilterList");
    filter = input.value.toUpperCase();
    ul = document.getElementById("ItemSetupItems");
    ul.style.display = "";
    li = ul.getElementsByTagName('li');

    for (i = 0; i < li.length; i++) {
        a = li[i];
        if (a == null || a.innerHTML.toUpperCase().indexOf(filter) > -1) {
            li[i].style.display = "";
        } else {
            li[i].style.display = "none";
        }
    }

}

$(document).ready(function () {


    $("#history").hide();
    $("#history_select").hide();
    $("#home_select").hide();
    $("home").show();
    $("#add_btn").show();
    $("#check_btn").hide();
    $("#recipt").hide();
    $("#order_setup").hide();
    $("#trans").hide();
    $("#will_order").hide();
    $("#close_order").hide();
    $("#review_order_table").hide();
    $("#settled_btn").hide();
    $("#unsettled_btn").hide();
    $("#history_trans").hide();
    $("#to_settle").hide();
    $("#to_settle_orders").hide();
    $("#done_settling").hide();




   

    $("#orderSetupItems").on("click", ".restButton", function () {
        $("#orderSetupItems").hide();
        $("#orderSetupFilterList").val(this.innerText);
        $(this).siblings().removeClass('active');
        $(this).addClass('active');
    });

    $("#ItemSetupItems").on("click", ".restButton", function () {
        $("#ItemSetupItems").hide();
        $("#ItemSetupFilterList").val(this.innerText);
        $(this).siblings().removeClass('active');
        $(this).addClass('active');
    });

    $("#done_settling").click(function () {
        $("#done_settling").hide();
        $("#to_settle").hide();
        $("#history").show();

    });



    $("#history_btn").click(function () {
        $("#history").hide();
        $("#history_select").hide();
        $("#home_select").hide();
        $("#home").hide();
        $("#add_btn").hide();
        $("#check_btn").hide();
        $("#recipt").hide();
        $("#order_setup").hide();
        $("#trans").hide();
        $("#will_order").hide();
        $("#close_order").hide();
        $("#review_order_table").hide();
        $("#settled_btn").show();
        $("#unsettled_btn").show();
        $("#history_trans").show();
        $("#to_settle").hide();
        $("#to_settle_orders").hide();
        $("#done_settling").hide();
    });

    $(".items_list").click(function () {

        

        // Find a <table> element with id="myTable":
        var table = document.getElementById("user_order_details");

        // Create an empty <tr> element and add it to the 1st position of the table:
        var row = table.insertRow(1);

        // Insert new cells (<td> elements) at the 1st and 2nd position of the "new" <tr> element:
        var cell1 = row.insertCell(0);
        var cell2 = row.insertCell(1);
        var cell3 = row.insertCell(2);
        var cell4 = row.insertCell(3);

        // Add some text to the new cells:
        cell1.innerHTML = $("#ItemSetupFilterList").innerText;
        cell2.innerHTML = "2";
        cell3.innerHTML = "2";
        cell4.innerHTML = "2";

    });

    $("#settled_btn").click(function () {
        $("#settled_btn").hide();
        $("#unsettled_btn").hide();
        $("#history_trans").hide();
        $("#history").show();
    });
    $("#unsettled_btn").click(function () {
        $("#settled_btn").hide();
        $("#unsettled_btn").hide();
        $("#history_trans").hide();
        $("#to_settle_orders").show();


    });
    $(".settle_this").click(function () {
        $("#to_settle").show();
        $("#to_settle_orders").hide();
        $("#done_settling").show();
    });

    $("#close_order").click(function () {
        $("#settled_btn").show();
        $("#unsettled_btn").show();
        $("#history_trans").show();
        $("#history_select").hide();
        $("#home_select").hide();
        $("#home").hide();
        $("#add_btn").hide();
        $("#check_btn").hide();
        $("#recipt").hide();
        $("#order_setup").hide();
        $("#close_order").hide();
        $("#review_order_table").hide();
        CloseBroadcast(broadcastid);
    });

    $("#details").click(function () {
        var details = prompt("please type any special comments you would like to include in the order");
    });
    $(".order_details").click(function () {
        $("#history").hide();
        $("#history_select").hide();
        $("#home_select").hide();
        $("#home").hide();
        $("#add_btn").hide();
        $("#check_btn").hide();
        $("#recipt").show();
        $("#order_setup").hide();
    });
    $("#current_order_btn").click(function () {
        $("#history").hide();
        $("#history_select").hide();
        $("#home_select").hide();
        $("#home").show();
        $("#add_btn").show();
        $("#check_btn").hide();
        $("#recipt").hide();
        $("#order_setup").hide();
        $("#trans").hide();
        $("#will_order").hide();
        $("#close_order").hide();
        $("#review_order_table").hide();
        $("#history_trans").hide();
        $("#to_settle").hide();
        $("#to_settle_orders").hide();
        $("#done_settling").hide();
        LoadBroadcasts();
    });
    $("#add_btn").on("click", function () {
        $("#history").hide();
        $("#history_select").hide();
        $("#home_select").hide();
        $("#home").hide();
        $("#add_btn").hide();
        $("#check_btn").hide();
        $("#recipt").hide();
        $("#order_setup").show();
        GetRestaurants();
    });
    $("#order_btn").click(function () {
        $("#trans").hide();
        $("#home_select").show();
        $("#check_btn").show();

    });
    $("#review_btn").click(function () {
        $("#trans").hide();
        $("#will_order").show();
        $("#review_order_table").show();
        $("#close_order").show();
    });

    $("#done").on("click", function () {
        $("#history").hide();
        $("#history_select").hide();
        $("#home_select").hide();
        $("#home").show();
        $("#add_btn").show();
        $("#check_btn").hide();
        $("#recipt").hide();
        $("#order_setup").hide();
        var name = prompt("Please enter your name");
        var id = $($("#orderSetupItems").find("li.active")).attr("restid");
        var deadline = $("#timepicker1").val();
        AddBroadcast(id, deadline);

    });

    $(".orders").click(function () {
        broadcastid = $(this).attr('unique_id');
        restaurantid = $(this).attr('restaurantid');
        $("#history").hide();
        $("#history_select").hide();
        $("#home").hide();
        $("#add_btn").hide();
        $("#recipt").hide();
        $("#order_setup").hide();
        $("#trans").show();
        //LoadOrders(broadcastid);
        GetRestaurantItems(restaurantid);
    });

    $("#add_restaurant").click(function () {
        var new_res = prompt("Please enter the restaurant name");
        $('#orderSetupFilterList').val(new_res);
        AddRestaurant(new_res);
    });

    $("#add_item").click(function () {
        var new_item = prompt("Please enter the item name");
        $('#ItemSetupFilterList').val(new_item);
        AddRestaurantItem(new_item, restaurantid);
    });

    $(".pay").click(function () {
        
        $("#history").hide();
        $("#history_select").show();
        $("#home_select").hide();
        $("#home").hide();
        $("#add_btn").hide();
        $("#check_btn").hide();
        $("#recipt").hide();
    });
    $("#check_btn").click(function () {
        $("#history").hide();
        $("#history_select").hide();
        $("#home_select").hide();
        $("#home").show();
        $("#add_btn").show();
        $("#check_btn").hide();
        $("#recipt").hide();
        $("#order_setup").hide();
        GetRestaurantItems(restaurantid);
    });

    LoadBroadcasts();
});





    
