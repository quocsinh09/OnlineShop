//
var lock_user = {
    init: function () {
        lock_user.registerEvents();
    },
    registerEvents: function () {
        $('.btn-blockuser').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = $(this).data('id')
            $.ajax({
                url: "/Admin/User/ChangeStatus",
                data: { username: id },
                đataType: "json",
                type : "POST", 
                success: function (response) {
                    
                    //alert(response)
                    if (response.status) {
                        btn.text("khóa người dùng")
                        $('.user-status').val("Đang hoạt động")
                    }
                    else {
                        btn.text("Bỏ khóa người dùng")
                        $('.user-status').val("Đã bị khóa")
                    }

                }
            })
        });

    }
}
lock_user.init()

//Xet quyen nguoi dung
var user_regency = {
    init: function () {
        user_regency.registerEvents();
    },
    registerEvents: function () {
        if($('.full').on('click')){
            $('.full').on('click', function (e) {
                e.preventDefault();
                var btn = $(this);
                var id = $(this).data('id')
                $.ajax({
                    url: "/Admin/User/ChangeRegency",
                    data: { username: id, regency : 2},
                    đataType: "json",
                    type: "POST",
                    success: function (response) {
                        $('.user-regency').text('Toàn quyền')
                    }
                })
            });
        }
        else if ($('.staff').on('click')) {
            $('.staff').on('click', function (e) {
                e.preventDefault();
                var btn = $(this);
                var id = $(this).data('id')
                $.ajax({
                    url: "/Admin/User/ChangeRegency",
                    data: { username: id, regency: 1 },
                    đataType: "json",
                    type: "POST",
                    success: function (response) {
                        $('.user-regency').text('Nhân viên')
                    }
                })
            });
        }

    }
}
user_regency.init()

var delete_user = {
    init: function () {
        delete_user.registerEvents();
    },
    registerEvents: function () {
        $('.delete-user').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = $(this).data('id')
            $.ajax({
                url: "/Admin/User/Delete",
                data: { username: id },
                đataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.delete) {
                        alert("Đã xóa người dùng "+ id )
                    }
                    else {
                        alert("Bạn không có quyền xóa tài khoản")
                    }
                    
                }
            })
        });

    }
}
delete_user.init()

