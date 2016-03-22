package com.epam.jdi.uitests.testing.unittests.enums;

import com.epam.commons.linqinterfaces.JAction;
import com.epam.jdi.uitests.testing.unittests.CommonData;
import com.epam.jdi.uitests.web.selenium.preconditions.WebPreconditions;
import org.openqa.selenium.WebElement;

import java.util.function.Supplier;

import static com.epam.jdi.uitests.core.preconditions.PreconditionsState.alwaysMoveToCondition;
import static com.epam.jdi.uitests.testing.unittests.entities.User.DEFAULT_USER;
import static com.epam.jdi.uitests.testing.unittests.pageobjects.EpamJDISite.contactFormPage;
import static com.epam.jdi.uitests.testing.unittests.pageobjects.EpamJDISite.dates;
import static com.epam.jdi.uitests.web.settings.WebSettings.getDriver;

/**
 * Created by 12345 on 03.06.2015.
 */
public enum Preconditions implements WebPreconditions {
    HOME_PAGE("index.htm"),
    CONTACT_PAGE("page1.htm"),
    CONTACT_PAGE_FILLED(() -> WebPreconditions.checkUrl("page1.htm"), () -> {
        WebPreconditions.openUri("page1.htm");
        contactFormPage.name.newInput(DEFAULT_USER.name);
        contactFormPage.lastName.newInput(DEFAULT_USER.lastName);
        contactFormPage.description.newInput(DEFAULT_USER.description);
    }),
    METALS_AND_COLORS_PAGE("page2.htm"),
    DATES_PAGE("page4.htm"),
    SUPPORT_PAGE("page3.htm", true),
    SORTING_TABLE_PAGE("page7.htm"),
    DYNAMIC_TABLE_PAGE("page5.htm"),
    SIMPLE_PAGE("page6.htm"),
    DATES_PAGE_FILLED(() -> WebPreconditions.checkUrl("page4.htm"), () -> {
        WebPreconditions.openUri("page4.htm");
        WebElement datePicker = getDriver().findElement(dates.datepicker.getLocator());
        datePicker.clear();
        datePicker.sendKeys(CommonData.TEST_DATE);
    });

    public Supplier<Boolean> checkAction;
    public JAction moveToAction;

    Preconditions(Supplier<Boolean> checkAction, JAction moveToAction) {
        this.checkAction = checkAction;
        this.moveToAction = moveToAction;
        alwaysMoveToCondition = true;
    }

    Preconditions(String uri) {
        this(() -> WebPreconditions.checkUrl(uri), () -> WebPreconditions.openUri(uri));
    }

    Preconditions(String uri, boolean isStatic) {
        this(uri);
        alwaysMoveToCondition = !isStatic;
    }

    public Boolean checkAction() {
        return checkAction.get();
    }

    public void moveToAction() {
        moveToAction.invoke();
    }
}
