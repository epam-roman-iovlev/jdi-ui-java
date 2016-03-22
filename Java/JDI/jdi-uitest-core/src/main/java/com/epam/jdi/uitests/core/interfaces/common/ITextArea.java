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
package com.epam.jdi.uitests.core.interfaces.common;

import com.epam.jdi.uitests.core.annotations.JDIAction;

/**
 * Created by Roman_Iovlev on 7/6/2015.
 */
public interface ITextArea extends ITextField {
    /**
     * @param textLines Specify text lines (clear textArea before
     *                  Clear textarea and Input several lines of text in textarea
     */
    @JDIAction
    void inputLines(String... textLines);

    /**
     * @param textLine Specify text to add new line (without clearing previous)
     *                 Add text in textarea from new line
     */
    @JDIAction
    void addNewLine(String textLine);

    /**
     * @return Get lines of text in textarea
     */
    @JDIAction
    String[] getLines();
}