var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: '/admin/user/getall',
            type: "GET",
            datatype: "json"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "email", "width": "15%" },
            { "data": "phoneNumber", "width": "15%" },
            { "data": "company.name", "width": "15%" },
            { "data": "role", "width": "15%" },
            {
                "data": { id: "id", lockoutEnd: "lockoutEnd" },
                "render": function (data) {
                    const today = new Date().getTime();
                    const lockout = new Date(data.lockoutEnd).getTime();

                    const isLocked = lockout > today;

                    const lockUnlockButton = `
                        <a onclick="LockUnlock('${data.id}')" 
                           class="btn ${isLocked ? 'btn-danger' : 'btn-success'} text-white" 
                           style="cursor:pointer; width:100px;" 
                           title="${isLocked ? 'Unlock the user' : 'Lock the user'}">
                            <i class="bi ${isLocked ? 'bi-lock-fill' : 'bi-unlock-fill'}"></i> 
                            ${isLocked ? 'Lock' : 'Unlock'}
                        </a>
                    `;

                    const permissionButton = `
                        <a class="btn btn-secondary text-white" 
                           style="cursor:pointer; width:150px;" 
                           title="Manage user permissions">
                            <i class="bi bi-pencil-square"></i> Permission
                        </a>
                    `;

                    return `
                        <div class="text-center d-flex justify-content-evenly">
                            ${lockUnlockButton}
                            ${permissionButton}
                        </div>
                    `;
                },
                "width": "25%"
            }
        ],
        "responsive": true,
        "autoWidth": false,
        "language": {
            "emptyTable": "No data available in table",
            "loadingRecords": "Loading...",
            "processing": "Processing...",
        },
        "order": [[0, "asc"]]
    });
}

function LockUnlock(id) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, do it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                url: '/Admin/User/LockUnlock',
                data: JSON.stringify(id),
                contentType: "application/json",
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            });
        }
    });
}