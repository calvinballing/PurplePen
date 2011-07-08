/* Copyright (c) 2006-2008, Peter Golde
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without 
 * modification, are permitted provided that the following conditions are 
 * met:
 * 
 * 1. Redistributions of source code must retain the above copyright
 * notice, this list of conditions and the following disclaimer.
 * 
 * 2. Redistributions in binary form must reproduce the above copyright
 * notice, this list of conditions and the following disclaimer in the
 * documentation and/or other materials provided with the distribution.
 * 
 * 3. Neither the name of Peter Golde, nor "Purple Pen", nor the names
 * of its contributors may be used to endorse or promote products
 * derived from this software without specific prior written permission.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND
 * CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES,
 * INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF
 * MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
 * BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
 * NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE
 * USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY
 * OF SUCH DAMAGE.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PurplePen
{
    partial class CourseSelector: UserControl
    {
        private bool showAllControls;
        private bool showCourseParts;
        private EventDB eventDB;

        private bool loaded = false;

        public CourseSelector()
        {
            InitializeComponent();
        }

        public bool ShowAllControls
        {
            get
            {
                return showAllControls;
            }
            set
            {
                showAllControls = value;
            }
        }

        public bool ShowCourseParts
        {
            get
            {
                return showCourseParts;
            }
            set
            {
                showCourseParts = value;
            }
        }

        internal EventDB EventDB
        {
            get
            {
                return eventDB;
            }
            set
            {
                eventDB = value;
            }
        }

        // Get or set the selected courses.
        // UNDONE MAPEXCHANGE - remove use of this property
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public Id<Course>[] SelectedCourses
        {
            get {
                CourseDesignator[] array = SelectedCourseDesignators;
                Id<Course>[] result = new Id<Course>[array.Length];
                for (int i = 0; i < array.Length; ++i)
                    result[i] = array[i].CourseId;
                return result;
            }

            set
            {
                CourseDesignator[] array = new CourseDesignator[value.Length];
                for (int i = 0; i < array.Length; ++i)
                    array[i] = new CourseDesignator(value[i]);
                SelectedCourseDesignators = array;
            }
        }

        // Get or set the selected courses designator
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public CourseDesignator[] SelectedCourseDesignators
        {
            get
            {
                LoadList();

                List<CourseDesignator> list = new List<CourseDesignator>();

                foreach (TreeNode node in courseTreeView.Nodes) {
                    if (node.Checked){
                        list.Add((CourseDesignator)(node.Tag));
                    }
                }

                return list.ToArray();
            }

            set
            {
                LoadList();

                foreach (TreeNode node in courseTreeView.Nodes) {
                    node.Checked = Array.IndexOf(value, ((CourseDesignator) (node.Tag))) >= 0;
                }
            }
        }

        private void selectAll_Click(object sender, EventArgs e)
        {
            foreach (TreeNode node in courseTreeView.Nodes)
                node.Checked = true;
        }

        private void selectNone_Click(object sender, EventArgs e)
        {
            foreach (TreeNode node in courseTreeView.Nodes)
                node.Checked = false;
        }

        private void CourseSelector_Load(object sender, EventArgs e)
        {
            LoadList();
        }

        private void LoadList()
        {
            if (eventDB != null && !loaded) {
                if (showAllControls) {
                    courseTreeView.Nodes.Add(new TreeNode(MiscText.AllControls) {Tag = CourseDesignator.AllControls});
                }

                foreach (Id<Course> courseId in QueryEvent.SortedCourseIds(eventDB)) {
                    TreeNode[] parts = null;

                    // If the course has parts, get all the parts.
                    int numberParts = QueryEvent.CountCourseParts(eventDB, courseId);
                    if (showCourseParts && numberParts > 1) {
                        parts = new TreeNode[numberParts];
                        for (int part = 0; part < numberParts; ++part) {
                            parts[part] = new TreeNode(string.Format(MiscText.PartN, part))
                            {
                                Tag = new CourseDesignator(courseId, part),
                                Checked = true
                            };
                        }
                    }

                    // Add node for the course to the tree.
                    TreeNode node;
                    if (parts != null)
                        node = new TreeNode(eventDB.GetCourse(courseId).name, parts);
                    else
                        node = new TreeNode(eventDB.GetCourse(courseId).name);

                    node.Tag = new CourseDesignator(courseId);
                    node.Checked = true;
                    courseTreeView.Nodes.Add(node);
                }

                courseTreeView.ExpandAll();
                loaded = true;
            }
        }

        // Prevent tree nodes from being collapsed.
        private void courseTreeView_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;
        }

        private bool inCheckUpdating = false;

        private void courseTreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (!inCheckUpdating) {
                inCheckUpdating = true;

                TreeNode node = e.Node;
                if (node.Level == 0)
                    UpdateChildNodes(node);
                else
                    UpdateNodeBasedOnChildren(node.Parent);

                inCheckUpdating = false;
            }
        }

        // Set all children to checked/unchecked based on the current node state.
        void UpdateChildNodes(TreeNode node)
        {
            bool isChecked = node.Checked;
            foreach (TreeNode childNode in node.Nodes)
                childNode.Checked = isChecked;
        }

        // Update parent node to checked iff all children are checked.
        void UpdateNodeBasedOnChildren(TreeNode parent)
        {
            bool anyUnchecked = false;

            foreach (TreeNode childNode in parent.Nodes)
                if (!childNode.Checked)
                    anyUnchecked = true;

            parent.Checked = !anyUnchecked;
        }
    }
}
