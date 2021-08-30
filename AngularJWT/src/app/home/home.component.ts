import { Component } from '@angular/core';
import { first } from 'rxjs/operators';

import { User } from '@app/_models';
import { UserService } from '@app/_services';
import $ from "jquery";
import _ from 'lodash';

@Component({ templateUrl: 'home.component.html' })
export class HomeComponent {
    loading = false;
    users: User[];


    constructor(private userService: UserService) { }

    ngOnInit() {
        this.loading = true;
        this.userService.getAll().pipe(first()).subscribe(users => {
            this.loading = false;
            this.users = users;
        });

        $('.is-img').on('click', function (e) {
            let parentOffset = $(this).offset(); 
            let cloneBtn = $('.btn-info-img').clone()
            $(this).append(cloneBtn.css({ 'top': (e.pageY - parentOffset.top - 16) + 'px', 'left': (e.pageX - parentOffset.left - 16) + 'px' }))
        })
    }
}