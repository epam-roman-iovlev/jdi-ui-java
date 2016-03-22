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

package com.epam.jdi.uitests.web.selenium.elements.composite;

import com.epam.jdi.uitests.core.interfaces.Application;
import com.epam.jdi.uitests.web.selenium.elements.WebCascadeInit;

import static com.epam.jdi.uitests.web.settings.WebSettings.getDriverFactory;

/**
 * Created by Roman_Iovlev on 8/30/2015.
 */
public class WebSite extends Application {
    public static <T> void Init(Class<T> site) {
        new WebCascadeInit().initStaticPages(site, getDriverFactory().currentDriverName());
        currentSite = site;
    }
    public static <T extends WebSite> T Init(Class<T> site, String driverName) {
        return new WebCascadeInit().initPages(site, driverName);
    }

}