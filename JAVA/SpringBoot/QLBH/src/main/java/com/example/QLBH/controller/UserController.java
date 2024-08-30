package com.example.QLBH.controller;


import com.example.QLBH.Role;
import com.example.QLBH.model.Product;
import com.example.QLBH.model.User;
import com.example.QLBH.service.UserService;
import jakarta.validation.Valid;
import jakarta.validation.constraints.NotNull;
import lombok.RequiredArgsConstructor;
import org.springframework.context.support.DefaultMessageSourceResolvable;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.validation.BindingResult;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.Optional;

@Controller
@RequestMapping("/")
@RequiredArgsConstructor
public class UserController {
    private final UserService userService;
    @GetMapping("/login")
    public String login() {
        return "users/login";
    }
    @GetMapping("/register")
    public String register(@NotNull Model model) {
        model.addAttribute("user", new User());
        return "users/register";
    }
    @PostMapping("/register")
    public String register(@Valid @ModelAttribute("user") User user,
                           @NotNull BindingResult bindingResult,

                           Model model) {
        if (bindingResult.hasErrors()) {
            var errors = bindingResult.getAllErrors()
                    .stream()

                    .map(DefaultMessageSourceResolvable::getDefaultMessage)
                    .toArray(String[]::new);
            model.addAttribute("errors", errors);
            return "users/register"; }
        userService.save(user);
        userService.setRoleRegister(user);
        return "redirect:/login";
    }
    @GetMapping("/users")
    public String userList(Model model) {
        List<User> users = userService.getUserandAdmin();
        model.addAttribute("users", users);

        return "users/users-list";
    }
    @GetMapping("/users/upRole/{id}")
    public String upRole(@PathVariable("id") Long id, Model model) {
        Optional<User> user = userService.findById(id);
        user.get().getRoles().contains(Role.USER.value);
        userService.upRole(id);
        List<User> users = userService.getUserandAdmin();
        model.addAttribute("users", users);
        return "users/users-list";
    }
    @GetMapping("/users/downRole/{id}")
    public String downRole(@PathVariable("id") Long id, Model model) {
        userService.downRole(id);
        List<User> users = userService.getUserandAdmin();
        model.addAttribute("users", users);
        return "users/users-list";
    }
    @GetMapping("/users/delete/{id}")
    public String deleteUser(@PathVariable("id") Long id, Model model) {
        userService.deleteUser(id);
        model.addAttribute("users", userService.findAll());
        return "users/users-list";
    }
}
