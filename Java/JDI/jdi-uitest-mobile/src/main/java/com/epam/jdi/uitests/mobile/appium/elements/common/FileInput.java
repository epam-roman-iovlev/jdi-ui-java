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

package com.epam.jdi.uitests.mobile.appium.elements.common;

import com.epam.jdi.uitests.core.interfaces.common.IFileInput;
import org.openqa.selenium.By;
import org.openqa.selenium.WebElement;

/**
 * Created by Roman_Iovlev on 7/10/2015.
 */
public class FileInput extends TextField implements IFileInput {
    public FileInput() {
        super();
    }

    public FileInput(By byLocator) {
        super(byLocator);
    }

    public FileInput(WebElement webElement) {
        super(webElement);
    }

    @Override
    protected void setValueAction(String value) {
        input(value);
    }
}