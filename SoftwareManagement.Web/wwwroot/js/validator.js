function validate(control) {
    var result = true;
    $(control).find('input:text, input:password, input:file, textarea')
        .each(function () {
            if ($(this).prop('required') && ($(this).val() == undefined || $(this).val() == null || $(this).val() == '')) {
                alert($(this).attr('data-msg'));
                $(this.focus());
                result = false;
                return result;
            }
        });
    $(control).find('input:radio, input:checkbox').each(function () {
        if ($(this).prop('required') && ($(this).val() == undefined || $(this).val() == null)) {
            alert($(this).attr('data-msg'));
            $(this.focus());
            result = false;
            return result;
        }

    });

    $(control).find('select').each(function () {
        if ($(this).prop('required') && $(this).val() <= 0) {
            alert($(this).attr('data-msg'));
            $(this.focus());
            result = false;
            return result;
        }
    });
    return result;
}