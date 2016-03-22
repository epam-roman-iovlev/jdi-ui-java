package com.epam.jdi.uitests.testing.unittests.tests.common;

import com.epam.jdi.uitests.core.interfaces.common.ILink;
import com.epam.jdi.uitests.testing.unittests.InitTests;
import com.epam.jdi.uitests.testing.unittests.tests.complex.CommonActionsData;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.Factory;
import org.testng.annotations.Test;

import java.lang.reflect.Method;

import static com.epam.jdi.uitests.testing.unittests.enums.Preconditions.HOME_PAGE;
import static com.epam.jdi.uitests.testing.unittests.enums.Preconditions.SUPPORT_PAGE;
import static com.epam.jdi.uitests.testing.unittests.pageobjects.EpamJDISite.*;
import static com.epam.jdi.uitests.testing.unittests.tests.complex.CommonActionsData.runParallel;
import static com.epam.jdi.uitests.testing.unittests.tests.complex.CommonActionsData.waitTimeOut;
import static com.epam.jdi.uitests.core.preconditions.PreconditionsState.isInState;
import static com.epam.web.matcher.testng.Assert.areEquals;
import static com.epam.web.matcher.testng.Assert.isTrue;

public class LinkTests extends InitTests {
    private ILink link() { return footer.about; }

    @BeforeMethod
    public void before(final Method method) {
        isInState(HOME_PAGE, method);
    }

    @Test
    public void clickTest() {
        link().click();
        supportPage.checkOpened();
    }

    // reference

    @Test
    public void getReferenceTest() {
        areEquals(link().getReference(), supportPage.url);
    }

    @Test
    public void waitReferenceTest() {
        isInState(SUPPORT_PAGE);
        runParallel(homePage::open);
        areEquals(() -> link().waitReferenceContains("page3.htm"), supportPage.url);
        isTrue(CommonActionsData.timer.timePassedInMSec() > waitTimeOut);
    }

    @Test
    public void waitMatchReferenceTest() throws Exception {
        isInState(SUPPORT_PAGE);
        runParallel(homePage::open);
        areEquals(() -> link().waitMatchReference(".*htm"), supportPage.url);
        isTrue(CommonActionsData.timer.timePassedInMSec() > waitTimeOut);
    }

    @Factory
    public Object[] textTests() {
        return new Object[]{
                new TextTests("Link", HOME_PAGE, this::link, "About", "Abou", "Abou.*")
        };
    }
}