import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { IUserRegistration } from "../../../models/user-registration.models";
import { IRegisterErrorResponse } from "../../../models/user-management-api.models";

@Component({
    selector: "app-register",
    templateUrl: "./register.component.html",
    styleUrls: ["./register.component.scss"],
})
export class RegisterComponent implements OnInit {
    private EMAIL_REGEX: RegExp = /^ *([A-Za-z]|\d|[_%+-])+(\.([A-Za-z]|\d|[_%+-])+)*@([A-Za-z]|\d|[-])+(\.([A-Za-z]|\d|[-])+)*(\.[A-Za-z]{2,4}) *$/;

    registerForm: FormGroup;

    @Output() submitted = new EventEmitter<IUserRegistration>();

    @Input() public pending: boolean;

    @Input() public errorMessage: IRegisterErrorResponse;

    constructor(private formBuilder: FormBuilder) {}

    ngOnInit() {
        this.createForm();
    }

    private createForm() {
        this.registerForm = this.formBuilder.group(
            {
                email: [
                    "",
                    Validators.compose([Validators.required, Validators.email, Validators.pattern(this.EMAIL_REGEX)]),
                ],
                password: ["", Validators.compose([Validators.required, Validators.minLength(8)])],
                confirmPassword: ["", Validators.compose([Validators.required])],
                firstName: [
                    "",
                    Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(15)]),
                ],
                lastName: [
                    "",
                    Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(15)]),
                ],
                location: [
                    "",
                    Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(15)]),
                ],
            },
            { validator: this.checkPasswords },
        );
    }

    onSubmit() {
        this.submitted.emit({
            email: this.registerForm.controls["email"].value,
            password: this.registerForm.controls["password"].value,
            firstName: this.registerForm.controls["firstName"].value,
            lastName: this.registerForm.controls["lastName"].value,
            location: this.registerForm.controls["location"].value,
        });

        this.registerForm.markAsPristine();
    }

    checkPasswords(group: FormGroup) {
        if (!group || !group.controls || !group.controls.password || !group.controls.confirmPass) {
            return null;
        }
        const pass = group.controls.password.value;
        const confirmPass = group.controls.confirmPassword.value;

        return pass === confirmPass ? null : { notSame: true };
    }
}
