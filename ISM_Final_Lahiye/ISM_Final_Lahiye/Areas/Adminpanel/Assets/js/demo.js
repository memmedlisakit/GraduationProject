$(document).ready(function () { 

    $(".page-header .navbar-nav .dropdown-menu .dropdown-item").removeClass("waves-effect waves-light");

    // Role delete button 
    $("#role #dataTable").on("click", ".activeAccount", function () {
        var id = $(this).data("id");
        $("#delete_link_a").attr("href", "/Role/Delete/" + id);
    }); 

    $("#delete_cancel").on("click", function () {
        $("#delete_link_a").attr("href", "#");
    })


    // User delete button  

    $("#user #dataTable").on("click", ".activeAccount", function () {
        var id = $(this).data("id");
        $("#delete_user").attr("href", "/User/Delete/" + id);
    }); 
    $("#cancel_user").on("click", function () {
        $("#delete_user").attr("href", "#");
    })


    
    // Role and Permission table delete button


    $("#roleAndPermission #dataTable").on("click", ".activeAccount", function () { 
            var id = $(this).data("id");
            $("#delete_link_a").attr("href", "/RoleAndPermission/Delete/" + id);
    });
 
    $("#cancel_user").on("click", function () {
        $("#delete_link_a").attr("href", "#");
    })




    // Role and User table delete button

    $("#roleAndUser #dataTable").on("click", ".activeAccount", function () {
        var id = $(this).data("id");
        $("#delete_link_a").attr("href", "/RoleAndUser/Delete/" + id);
    });
     
    $("#cancel_user").on("click", function () {
        $("#delete_link_a").attr("href", "#");
    })




    // Brand table delete button

    $("#brand #dataTable").on("click", ".activeAccount", function () {
        var id = $(this).data("id");
        $("#delete_brand").attr("href", "/Brand/Delete/" + id);
    });

    $("#delete_cancel_brand").on("click", function () {
        $("#delete_brand").attr("href", "#");
    })


    // delete product 
    $("#product #dataTable").on("click", ".activeAccount", function () {
        var id = $(this).data("id");
        $("#delete_product").attr("href", "/Product/Delete/" + id);
    });

    $("#cancel_product").on("click", function () {
        $("#delete_product").attr("href", "#");
    })



    //  check username avelable or is not avelable

    $(".input_username").on("change paste keyup", function () {
        var text = $(this).val();
        var rootUrl = "/User/check_username";
        $.ajax({
            type: "POST", 
            url: rootUrl, 
            data: {username: text },
            success: function (data) { 
                if (data == "Username is available") {
                    $("#label").attr("class", "alert alert-success");
                    $("#label").text(data);
                    $("#add_btn").prop("disabled", false);
                } else {
                    $("#label").removeAttr("class");
                    $("#label").attr("class", "alert alert-danger");
                    $("#label").text(data);
                    $("#add_btn").prop("disabled", true);
                }
               
                if ($(".input_username").val() == "") {
                    $("#label").removeAttr("class");
                    $("#label").text("");
                } 
                //window.location.replace("/User/Insert");
            },
        }) 
    });   

    

    

    // product color picker
    var colors = ["#f44242", "#f4e841", "#73f441", "#41f4df", "#418ef4", "#f441d3"]
    for (var i = 0; i < $(".color").length; i++) {
        $($(".color")[i]).css("background-color", colors[i]);
        $($(".color")[i]).on("click", function () {
            $("#color_icon").css("color", $(this).css("background-color"));
            $("#product_color").val($(this).css("background-color"));
        })
    }  


    // product count iteration 
    $(".minus_count").on("click", function () { 
        var quantity_td = $($($(this).parent().parents()[3]).children()[3]);
        var input = $(this).parent().next();

        var quantity = $(quantity_td).text();
        var number = $(input).val();

        if (number != 0) { 
            number--;
            quantity++;
            $(input).val(number);
            $(quantity_td).text(quantity);
        }  
    })

    $(".plus_count").on("click", function () {
        var quantity_td = $($($(this).parent().parents()[3]).children()[3]);
        var input = $(this).parent().prev(); 


        var quantity = $(quantity_td).text();
        var number = $(input).val(); 

        if (number != 10 && quantity != 0) {
            number++;
            quantity--;
            $(input).val(number);
            $(quantity_td).text(quantity);
        }  
    })


    //  add to cart button
    $(".add_cart").on("click", function () {
        var product_quantity = $($($($(this).parents("tr").children()[6]).find("input"))[0]).val();
        var product_id = $(this).data("product_id");
        var root_url = "/Product/Add_to_cart";


        if (product_quantity > 0) {
            $.ajax({
                url: root_url,
                type: "POST",
                data: { product_quantity, product_id },
                success: function () {
                    var shoppint_cart = $("#sopping_cart_count");
                    var count = shoppint_cart.text();
                    count++;
                    shoppint_cart.text(count);
                },
                error: function () {
                    $("#modal_title").text("Server Error");
                    $("#modal_text").text("An error occurred with connecting database");
                    $("#cart_error").modal("show");
                }
            })
        } else {
            $("#modal_title").text("Cart Error");
            $("#modal_text").text("Please enter product quantity");
            $("#cart_error").modal("show");
        }
    })

    // order it now button product order
    $(".order_it_now").on("click", function () {
        var user_id = $(this).data("user_id");
        var product_id = $(this).data("product_id");
        var product_quantity = $(this).data("product_quantity");
        var total_price = $(this).data("total_price");
        var cart_id = $(this).data("cart_id"); 
        var elemet = $(this);

        var root_url = "/Product/Order"; 
        $.ajax({
            url: root_url,
            type: "POST",
            data: { user_id, product_id, product_quantity, total_price, cart_id },
            success: function () {
                $($(elemet).parents(".row.mt-4")[0]).remove();
                var count = $("#sopping_cart_count").text();
                count--;
                $("#sopping_cart_count").text(count);
            },
            error: function () {
                $("#ajax_error").modal("show");
            }
        })
    })


    // order check status send or not send
    $(".order_check_status").on("click", function () {
        var status_label = $($(this).prev());
        var order_id = $(this).data("order_id");
        var root_url = "/Order/Order_Update";
        var object = $(this);
        $.ajax({
            url: root_url,
            type: "POST",
            data: {order_id },
            success: function () {
                if ($(object).hasClass("checked")) {
                    $(object).removeClass("checked");
                    $(status_label).removeClass("badge-success").addClass("badge-warning");
                    $(status_label).children("i").removeClass("fa-check-square").addClass("fa-spinner");
                } else {
                    $(object).addClass("checked");
                    $(status_label).removeClass("badge-warning").addClass("badge-success");
                    $(status_label).children("i").removeClass("fa-spinner").addClass("fa-check-square");
                } 
            },
            error: function () {
                $("#order_status").modal("show");
                $("#order_status #modal_title").text("Server error");
                $("#order_status #modal_text").text("An error occurred connecting to database");
                
            }
        }) 
    }); 



    // delete order button
    $(".delete_order").on("click", function () {
        var order_id = $(this).data("delete_order_id");
        var root_url = "/Order/Delete_Order";
        var object = $(this); 
        $.ajax({
            url: root_url,
            type: "POST",
            data: { order_id },
            success: function () {
                $($(object).parents(".col-md-6")[0]).remove();
            },
            error: function () {
                $("#order_status").modal("show");
                $("#order_status #modal_title").text("Server error");
                $("#order_status #modal_text").text("An error occurred connecting to database");
            }
        })


    })


    // change password ajax
    $("#save_password").on("click", function () {
        var message_label = $("#message_label");
        var new_password = $("#new_password").val();
        var confirm_password = $("#confirm_password").val();
        if (new_password == confirm_password) {
            if (new_password.length >= 5) {
                var root_url = "/User/Update_password"
                $.ajax({
                    url: root_url,
                    type: "POST",
                    data: { new_password },
                    success: function (text) {
                        message_label.removeClass("badge-danger").addClass("badge-success");
                        message_label.text("Your password changed succsesfuly"); 
                    },
                    error: function () {
                        message_label.removeClass("badge-success").addClass("badge-danger");
                        message_label.text("An error occurred connecting database");
                    }
                }) 
            } else {
                message_label.removeClass("badge-success").addClass("badge-danger");
                message_label.text("Password length must be more than 5 character");
            } 
        } else {
            message_label.removeClass("badge-success").addClass("badge-danger");
            message_label.text("Password not same");
        } 
    })
});
