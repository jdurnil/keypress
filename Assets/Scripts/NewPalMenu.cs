/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * Licensed under the Oculus SDK License Agreement (the "License");
 * you may not use the Oculus SDK except in compliance with the License,
 * which is provided at the time of installation or download, or which
 * otherwise accompanies this software in either electronic or hard copy form.
 *
 * You may obtain a copy of the License at
 *
 * https://developer.oculus.com/licenses/oculussdk/
 *
 * Unless required by applicable law or agreed to in writing, the Oculus SDK
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using UnityEngine;

namespace Oculus.Interaction.Samples.PalmMenu
{
    /// <summary>
    /// Example of a bespoke behavior created to control a particular palm menu. This menu primarily controls the swiping behavior,
    /// showing and hiding various options and controlling the pagination dots depending on the state of the menu. Note that, for
    /// buttons with several possible icons, the states of those buttons are controlled by the PalmMenuExampleButtonHandlers script,
    /// which manages the state of the various handlers.
    /// </summary>
    public class NewPalMenu : MonoBehaviour
    {


        [SerializeField]
        private GameObject _menuParent;

        [SerializeField]
        private AudioSource _showMenuAudio;

        [SerializeField]
        private AudioSource _hideMenuAudio;



        private void Start()
        {

            _menuParent.SetActive(false);
        }

        private void Update()
        {
           
        }

       

        /// <summary>
        /// Show/hide the menu.
        /// </summary>
        public void ToggleMenu()
        {
            if (_menuParent.activeSelf)
            {
                _hideMenuAudio.Play();
                _menuParent.SetActive(false);
            }
            else
            {
                _showMenuAudio.Play();
                _menuParent.SetActive(true);
            }
        }
    }
}
