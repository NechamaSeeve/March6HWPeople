let i = 0;
$(() => {
    
    
    $("#add-rows").on('click', function () {
        
       i++;

        const html = ` <div id="ppl-rows">
                        <div class="row person-row" style="margin-bottom: 10px;">
                            <div class="col-md-4">
                                <input class="form-control" type="text" name="people[${i}].firstname" placeholder="First Name" />
                            </div>
                            <div class="col-md-4">
                                <input class="form-control" type="text" name="people[${i}].lastname" placeholder="Last Name" />
                            </div>
                            <div class="col-md-4">
                                <input class="form-control" type="text" name="people[${i}].Age" placeholder="Age" />
                            </div>
                        </div>

                    </div>`;

        $('#ppl-rows').append(`${html}`)
       
        
    });
});
