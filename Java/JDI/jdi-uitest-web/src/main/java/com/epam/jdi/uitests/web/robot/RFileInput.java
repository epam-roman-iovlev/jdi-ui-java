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

package com.epam.jdi.uitests.web.robot;

import com.epam.jdi.uitests.core.interfaces.common.ITextField;
import com.epam.jdi.uitests.web.selenium.elements.common.FileInput;
import org.openqa.selenium.By;

import static com.epam.jdi.uitests.web.robot.JRobot.pasteText;

/**
 * Text Field control implementation
 *
 * @author Alexeenko Yan
 * @author Shubin Konstantin
 * @author Zharov Alexandr
 */
public class RFileInput extends FileInput implements ITextField {
    public RFileInput() {
        super();
    }

    public RFileInput(By byLocator) {
        super(byLocator);
    }

    @Override
    protected void clearAction() {
    }
    @Override
    protected void inputAction(CharSequence text) {
        getWebElement().click();
        pasteText(text);
    }

}