using EcoLogTracking.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcoLogTracking.Server.Controllers.Impl
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThemeController : ControllerBase
    {
        private static readonly ThemeColors _standardColors = new ThemeColors
        {
            PrimaryColor = "#a1f097",
            MainBoxColor = "#03e9f4",
            ButtonBackgroundColor = "#6fef88",
            ButtonBackgroundHoverColor = "#b7f8ce",
            ShadowColor = "#69eb6d"
        };

        private static readonly ThemeColors _dangerColors = new ThemeColors
        {
            PrimaryColor = "#ff6347",
            SecondaryColor = "red",
            MainBoxColor = "#ff6347",
            ButtonBackgroundColor = "#ff6347",
            ButtonBackgroundHoverColor = "#ff6347",
            ShadowColor = "#ff6347"
        };

        [HttpGet("standard")]
        public ActionResult<ThemeColors> GetStandardColors()
        {
            return Ok(_standardColors);
        }

        [HttpGet("danger")]
        public ActionResult<ThemeColors> GetDangerColors()
        {
            return Ok(_dangerColors);
        }

        [HttpPost("standard")]
        public ActionResult SetStandardColors([FromBody] ThemeColors themeColors)
        {
            // Actualizamos los colores estándar
            _standardColors.PrimaryColor = themeColors.PrimaryColor;
            _standardColors.SecondaryColor = themeColors.SecondaryColor;
            _standardColors.MainBoxColor = themeColors.MainBoxColor;
            _standardColors.ButtonBackgroundColor = themeColors.ButtonBackgroundColor;
            _standardColors.ButtonBackgroundHoverColor = themeColors.ButtonBackgroundHoverColor;
            _standardColors.ShadowColor = themeColors.ShadowColor;
            return NoContent();
        }

        [HttpPost("danger")]
        public ActionResult SetDangerColors([FromBody] ThemeColors themeColors)
        {
            // Actualizamos los colores "danger"
            _dangerColors.PrimaryColor = themeColors.PrimaryColor;
            _dangerColors.SecondaryColor = themeColors.SecondaryColor;
            _dangerColors.MainBoxColor = themeColors.MainBoxColor;
            _dangerColors.ButtonBackgroundColor = themeColors.ButtonBackgroundColor;
            _dangerColors.ButtonBackgroundHoverColor = themeColors.ButtonBackgroundHoverColor;
            _dangerColors.ShadowColor = themeColors.ShadowColor;
            return NoContent();
        }
    }
}
