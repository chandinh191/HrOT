using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using hrOT.Domain.IdentityModel;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;

namespace hrOT.Application.Employees.Commands.Update
{
    public record UpLoadImage : IRequest
    {
        public IFormFile File { get; set; }
    }

    public class UpLoadImageHandler : IRequestHandler<UpLoadImage>
    {
        private readonly IApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHostingEnvironment _environment;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpLoadImageHandler(IApplicationDbContext context, UserManager<ApplicationUser> userManager, IHostingEnvironment environment, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            this.userManager = userManager;
            _environment = environment;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<Unit> Handle(UpLoadImage request, CancellationToken cancellationToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var employeeIdCookie = httpContext.Request.Cookies["EmployeeId"];
            var employeeIdGuid = Guid.Parse(employeeIdCookie);
            var entity = await _context.Employees
                .Include(e => e.ApplicationUser)
                .FirstOrDefaultAsync(e => e.Id == employeeIdGuid, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Employee), employeeIdGuid);
            }

            // Lấy đường dẫn tuyệt đối của thư mục wwwroot
            string wwwrootPath = _webHostEnvironment.WebRootPath;

            string webpFolderPath = Path.Combine(wwwrootPath, "Images");

            // Tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(webpFolderPath))
            {
                Directory.CreateDirectory(webpFolderPath);
            }

            // Định nghĩa đường dẫn và tên file cho hình ảnh sau khi chuyển đổi sang định dạng WebP
            string webpImageFileName = $"{Guid.NewGuid()}.webp";
            string webpImagePath = Path.Combine(webpFolderPath, webpImageFileName);

            // Chuyển đổi hình ảnh sang định dạng WebP
            using (Image image = Image.Load(request.File.OpenReadStream()))
            {
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(800, 600),
                    Mode = ResizeMode.Max
                }));

                // Lưu hình ảnh dưới dạng định dạng WebP
                image.Save(webpImagePath);
            }

            if (entity.ApplicationUser != null)
            {
                entity.ApplicationUser.Image = "/Images/" + webpImageFileName;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}