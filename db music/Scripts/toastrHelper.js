function showNotification(message, title, type) {
    switch (type) {
        case "Info":
            toastr.info(message, title);
            break;
        case "Success":
            toastr.success(message, title);
            break;
        case "Warning":
            toastr.warning(message, title);
            break;
        case "Error":
            toastr.error(message, title);
            break;
    }
}