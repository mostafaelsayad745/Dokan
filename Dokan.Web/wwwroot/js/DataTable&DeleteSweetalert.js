// Generic DataTable and Delete Handler - Reusable across all pages

$(function () {
    // Initialize DataTable for any table with class "data-table"
    $(".data-table").DataTable({
        "responsive": true,
        "lengthChange": false,
        "autoWidth": false,
        "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"]
    }).buttons().container().appendTo('.datatable_wrapper .col-md-6:eq(0)');
});

// Generic SweetAlert Delete Handler - Works for any delete link with class "delete-btn"
$(document).on('click', 'a.delete-btn', function (e) {
    e.preventDefault();
    let deleteUrl = $(this).attr('href');
    let itemName = $(this).data('item-name') || 'item'; // Get item name from data attribute

    Swal.fire({
        title: "Are you sure?",
        text: `You won't be able to revert this ${itemName}!`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "GET",
                url: deleteUrl,
                success: function (data) {
                    if (data.success) {
                        Swal.fire({
                            title: "Deleted!",
                            text: `Your ${itemName} has been deleted successfully.`,
                            icon: "success"
                        }).then(() => {
                            location.reload();
                        });
                    }
                    else {
                        Swal.fire({
                            title: "Error!",
                            text: data.message,
                            icon: "error"
                        });
                    }
                },
                error: function () {
                    Swal.fire({
                        title: "Error!",
                        text: `An error occurred while deleting the ${itemName}.`,
                        icon: "error"
                    });
                }
            });
        }
    });
});