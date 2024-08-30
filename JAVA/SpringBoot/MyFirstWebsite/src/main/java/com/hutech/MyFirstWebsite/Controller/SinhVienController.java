package com.hutech.MyFirstWebsite.Controller;

import com.hutech.MyFirstWebsite.Model.SinhVien;
import jakarta.validation.Valid;
import org.springframework.stereotype.Controller;
import org.springframework.validation.BindingResult;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.servlet.view.RedirectView;

import java.util.ArrayList;
import java.util.List;

@Controller
public class SinhVienController {
    private List<SinhVien> SinhVienList =new ArrayList<>();
    @GetMapping("/sinhvien")
    public String showForm(Model model) {
        model.addAttribute("sinhVien", new SinhVien());
        return "sinhvien/form-sinhvien";
    }
    @PostMapping("/sinhvien")
    public String submitForm(@Valid SinhVien sinhVien, BindingResult bindingResult,
                             Model model) {
        if (bindingResult.hasErrors()) {
            return "sinhvien/form-sinhvien";
        }
        for (SinhVien s : SinhVienList) {
            if(s.getTen().equals(sinhVien.getTen())) {
                SinhVienList.remove(s);
                SinhVienList.add(sinhVien);
                model.addAttribute("SinhVienList", SinhVienList);
                return "sinhvien/result-sinhvien";
            }
        }
        SinhVienList.add(sinhVien);
        model.addAttribute("SinhVienList", SinhVienList);
        return "sinhvien/result-sinhvien";
    }
    @PostMapping("/sinhvien/delete")
    public String delete(@RequestParam("id") String id, Model model) {
        int index = Integer.parseInt(id);
        SinhVienList.remove(index);
        model.addAttribute("SinhVienList", SinhVienList);
        return "/sinhvien/result-sinhvien";
    }

}
