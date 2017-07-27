var broadcastid;
var restaurantid;
var email;
var typingTimer;                //timer identifier
var doneTypingInterval = 5000;  //time in ms, 5 second for example

function LoadRestaurants(restList) {
    
    if (restList != null && restList.length > 0)
    {
        $("#orderSetupFilterList").prop('disabled', false);
        $("#orderSetupFilterList").attr("placeholder", "seach for a retaurant...");
        $('#orderSetupItems li:not(:first)').remove();
        $("#orderSetupItems").append("<li class=\"restButton list-group-item active\" restId = " + restList[restList.length - 1].RestaurantID + ">" + restList[restList.length - 1].Name + "</li>");
        for (var i = restList.length - 2; i >= 0; i--) {
            var name = restList[i].Name;
            $("#orderSetupItems").append("<li class=\"restButton list-group-item\" restId = " + restList[i].RestaurantID + ">" + name + "</li>");
        }
    }
    else
    {
        $("#orderSetupFilterList").prop('disabled', true);
        $("#orderSetupFilterList").attr("placeholder", "Please add a restaurant...");
    }
}

function updateItemPriceEvent(btn) {
    var price = btn.val();
    var name = btn.parent().siblings().filter(":first")[0].innerHTML;
    if (price != null && name != null && price != "" && name != "")
    {
        AddItemPrice(restaurantid, name, price);
    }
}

function LoadBroadcastSummary(orders) {
    $('#review_order_table tbody').empty();
    for (var i = 0; i < orders.length; i++) {
        var name = orders[i].ItemName;
        var quantity = orders[i].Quantity;
        var comments = orders[i].Comments;
        var commentToBeWrittenInTable = "";
        if (comments == null || comments.length == 0) {
            commentToBeWrittenInTable = "No Comments For This Item";
        }
        else {
            commentToBeWrittenInTable = comments.slice('\n').join("<br />");
        }
        $("#review_order_table tbody").append("<tr><td><p>" + name + "</p></td><td><p>" + quantity + "</p></td><td>" + commentToBeWrittenInTable + "</td></tr>");
    }
}

function LoadTotalReceipt(receipt) {
    $("#total_receipt").empty();
    receiptItems = receipt.ReceiptItems;
    total = receipt.Total;
    for (i = 0; i < receiptItems.length; i++) {
        $("#total_receipt").append("<tr class=\"details order_details\"><td><a>" + receiptItems[i].Email + "</a></td><td><p>" + receiptItems[i].Total + "</p></td></tr>");
    }
    if (total != null && total > 0) {
        $("#history_total_amount").text("Total: " + total);
    }
    else {
        $("#history_total_amount").text("Total is undefined. Please set prices for all items");
    }

}

function LoadUserReceipt(items) {
    $('#user_receipt_table_body').empty();
    if (items != null) {
        for (var i = 0; i < items.length; i++) {
            var name = items[i].Item.Name;
            var quantity = items[i].Quantity;
            var price = items[i].Item.Price;
            var total = "";
            if (price != 0 && price != null) {
                total = quantity * price;
            }
            else {
                price = "";
            }

            $("#user_receipt_table_body").append("<tr><td>" + name + "</td><td>" + quantity + "</td><td><input type = \"number\" class=\"quantity_col_input\" min=\"1\" value=\"" + price + "\"></td><td>" + total + "</td></tr>");
        }
        $('summary_per_user').empty();
    }
}

function LoadOrders(orders) {
    $('#user_order_details').empty();
    if (orders != null)
    {
        for (var i = 0; i < orders.length; i++) {
            var name = orders[i].Item.Name;
            var quantity = orders[i].Quantity;
            var comments = orders[i].Comments;
            if (comments == null || comments == "") {
                comments = "";
            }

            $("#user_order_details").append("<tr><td>" + name + "</td><td><input type = \"number\" class=\"quantity_col_input\" min=\"1\" value=\"" + quantity + "\"></td><td><input type = \"text\" class=\"comments_col_input\" value=\"" + comments + "\"></td><td><button class='delete_item'>X</button></td>");
        }
    }
}

function LoadRestaurantItems(restList) {
    
    
    if (restList != null && restList.length > 0) {
        $("#ItemSetupFilterList").prop('disabled', false);
        $("#ItemSetupFilterList").attr("placeholder", "seach for an item...");
        $('#ItemSetupItems li:not(:first)').remove();
        $("#ItemSetupItems").append("<li class=\"restButton items_list list-group-item active\">" + restList[restList.length - 1] + "</li>");
        for (var i = restList.length - 2; i >= 0; i--) {
            $("#ItemSetupItems").append("<li class=\"restButton items_list list-group-item\">" + restList[i] );
        }
    }
    else
    {
        $("#ItemSetupFilterList").prop('disabled', true);
        $("#ItemSetupFilterList").attr("placeholder", "Please add an item");
        
    }
}


function initializeOrderList() {
    var input, filter, ul, li, a, i;
    input = document.getElementById("orderSetupFilterList");
    filter = input.value.toUpperCase();
    ul = document.getElementById("orderSetupItems");
    ul.style.display = "";
    li = ul.getElementsByTagName('li');

    for (i = 0; i < li.length; i++)
    {
        a = li[i];
        if (a == null || a.innerHTML.toUpperCase().indexOf(filter) > -1)
        {
            li[i].style.display = "";
        }
        else
        {
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

    $("#add_item").click(function () {
        var new_item = prompt("Please enter the item name");
        
        if (new_item != null) {
            new_item = new_item.toUpperCase();
            var input, filter, ul, li, a, i;
            input = document.getElementById("ItemSetupFilterList");
            filter = new_item.toUpperCase();
            ul = document.getElementById("ItemSetupItems");
            ul.style.display = "";
            li = ul.getElementsByTagName('li');

            var found = false;

            for (i = 0; i < li.length; i++) {
                a = li[i];
                if ((a == null || a.innerHTML.toUpperCase().indexOf(filter) > -1)) {
                    if (a.innerHTML.toUpperCase()==filter)
                    { found = true;}
                }
            }
            if (!found)//the item is not in the list add it to the list
            {
                $('#ItemSetupFilterList').val(new_item);
                AddRestaurantItem(new_item, restaurantid);
            }
            else {
                alert("This item is already in the list");
            }
        }
    });
    $("#orderSetupItems").on("click", ".restButton", function () {
        $("#orderSetupItems").hide();
        $("#orderSetupFilterList").val(this.innerText);
        $(this).siblings().removeClass('active');
        $(this).addClass('active');
        $("#done").prop("disabled", false);
        $("#msg").hide();
        $("#msg2").show();
    });
    $("#user_receipt_table_body").on("click", "button", function () {
        var price = prompt("Enter Price");
        var priced = parseFloat(price);
        AddItemPrice(restaurantid,$(this).parent().siblings().filter(":first")[0].innerHTML,priced);
        ReceiptDetail(broadcastid,email);

    });
    $("#ItemSetupItems").on("click", ".restButton", function () {

        $("#ItemSetupFilterList").val(this.innerText);
        $(this).siblings().removeClass('active');
        $(this).addClass('active');
        // Find a <table> element with id="myTable":
        var name = $("#ItemSetupFilterList").val();
        $("#user_order_details").append("<tr><td>" + name + "</td><td><input type = \"number\" class=\"quantity_col_input\"></td><td><input type = \"text\" class=\"comments_col_input\"></td><td><button class='delete_item'><p>X</p></button></td>");
        
        $("#check_btn").prop("disabled", false);
    });
    $("#done_settling").click(function () {
        $("#done_settling").hide();
        $("#to_settle").hide();
        $("#history").show();

    });
    $("#history_btn").click(function () {
        $("#history").show();
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
        LoadHistory();
        $("#msg").hide();
        $("#msg2").hide();


    });
    $("#close_order").click(function () {

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
        $("#history").show();

    });
    $("#details").click(function () {
        var details = prompt("please type any special comments you would like to include in the order");
    });
    $("#total_receipt").on("click", "tr", function () {
        email = $(this).closest('tr').children('td:first').text();
        ReceiptDetail(broadcastid, email);
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
        $("#msg").hide();
        $("#msg2").hide();
        
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
        $("#done").prop("disabled", true);
        $("#msg").show();
        $("#msg2").hide();
    });
    $("#order_btn").click(function () {
        $("#trans").hide();
        $("#home_select").show();
        $("#check_btn").show();
        
       


    });
    $("#review_btn").click(function () {
        GetBroadcastSummary(broadcastid);
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
        var id = $($("#orderSetupItems").find("li.active")).attr("restid");
        var deadline = $("#timepicker1").val();
        AddBroadcast(id, deadline);
        $("#orderSetupFilterList").val("");
        $('#orderSetupItems li:not(:first)').remove();
        $("#msg2").hide();

    });
    $(".table_history_select").on("click", "button", function () {
        $(this).closest('tr').remove();
        var names = $("#user_order_details tr td:nth-child(1)").map(function () {
            return $(this).text();
        }).get();
        var quantity = $(".quantity_col_input").map(function () {
            return parseInt($(this).val());
        }).get();
        var comments = $(".comments_col_input").map(function () {
            return $(this).val();
        }).get();
        AddOrder(names, quantity, comments, broadcastid);
    });
    $(".orders").on("click", "button", function () {
        broadcastid = $(this).attr('unique_id');
        restaurantid = $(this).attr('restaurantid');
        $("#history").hide();
        $("#history_select").hide();
        $("#home").hide();
        $("#add_btn").hide();
        $("#recipt").hide();
        $("#order_setup").hide();
        $("#trans").show();
        CurrentOrder(broadcastid);
        GetRestaurantItems(restaurantid);
    });
    $("#add_restaurant").click(function () {
        var new_res = prompt("Please enter the restaurant name");
        
        if (new_res != null)
        {
            new_res = new_res.toUpperCase();
            var input, filter, ul, li, a, i;
            input = document.getElementById("orderSetupFilterList");
            filter = new_res.toUpperCase();
            ul = document.getElementById("orderSetupItems");
            ul.style.display = "";
            li = ul.getElementsByTagName('li');
            var found = false;

            for (i = 0; i < li.length; i++)
            {
                a = li[i];
                if (a == null || a.innerHTML.toUpperCase().indexOf(filter) > -1)
                {
                    if (a.innerHTML.toUpperCase() == filter)
                    { found = true; }
                }
            }
            if(!found)//the item is not in the list add it to the list
            {
                $('#orderSetupFilterList').val(new_res);
                AddRestaurant(new_res);
            }
            else
            {
                alert("This restaurant is already in the list");
            }
        } 
    });
    $("#user_receipt_table_body").on("change", "input", function () {
        updateItemPriceEvent($(this));
    });
    $("#history").on("click", "button", function () {
        GetReciept($(this).attr('unique_id'));
        broadcastid = $(this).attr('unique_id');
        restaurantid = $(this).attr('restaurantid');
        $("#history").hide();
        $("#history_select").show();
        $("#home_select").hide();
        $("#home").hide();
        $("#add_btn").hide();
        $("#check_btn").hide();
        $("#recipt").hide();
    });
    $("#check_btn").click(function () {
        var names = $("#user_order_details tr td:nth-child(1)").map(function () {
            return $(this).text();
        }).get();
        var quantity = $(".quantity_col_input").map(function () {
            return parseInt($(this).val());
        }).get();
        var comments = $(".comments_col_input").map(function () {
            return $(this).val();
        }).get();
        AddOrder(names, quantity, comments, broadcastid);
        $("#history").hide();
        $("#history_select").hide();
        $("#home_select").hide();
        $("#check_btn").hide();
        $("#recipt").hide();
        $("#order_setup").hide();
        $("#trans").show();
        $("#ItemSetupFilterList").val("");
        $('#ItemSetupItems li:not(:first)').remove();
        $("#msg4").hide();
    });
    LoadBroadcasts();
});






