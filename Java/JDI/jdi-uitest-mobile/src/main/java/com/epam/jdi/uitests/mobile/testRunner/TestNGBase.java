/*
 * Copyright 2004-2016 EPAM Systems
 *
 * This file is part of JDI project.
 *
 * JDI is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * JDI is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; 
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with JDI. If not, see <http://www.gnu.org/licenses/>.
 */

package com.epam.jdi.uitests.mobile.testRunner;

import com.epam.commons.Timer;
import com.epam.jdi.uitests.mobile.WebSettings;
import org.testng.annotations.AfterSuite;
import org.testng.annotations.BeforeSuite;

import java.text.SimpleDateFormat;
import java.util.Date;

import static com.epam.commons.StringUtils.LINE_BREAK;
import static com.epam.jdi.uitests.core.settings.JDISettings.*;
import static com.epam.jdi.uitests.mobile.WebSettings.useDriver;
import static com.epam.jdi.uitests.mobile.appium.driver.DriverTypes.ANDROID;
import static com.epam.jdi.uitests.mobile.appium.driver.WebDriverUtils.killAllRunWebDrivers;

/**
 * Created by Roman_Iovlev on 9/3/2015.
 */
public class TestNGBase {
    protected static Timer timer;

    public static long getTestRunTime() {
        return timer.timePassedInMSec();
    }

    @BeforeSuite(alwaysRun = true)
    public static void jdiSetUp() throws Exception {
        WebSettings.init();
        logger.info("Init test run");
        killAllRunWebDrivers();
        initFromProperties();
        if (!driverFactory.hasDrivers())
            useDriver(ANDROID);
        timer = new Timer();
    }

    @AfterSuite(alwaysRun = true)
    public static void jdiTearDown() {
        logger.info("Test run finished. " + LINE_BREAK + "Total test run time: " +
                new SimpleDateFormat("HH:mm:ss.S").format(new Date(21 * 3600000 + getTestRunTime())));
        killAllRunWebDrivers();
    }}