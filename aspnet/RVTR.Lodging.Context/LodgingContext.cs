using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.Domain.Models;

namespace RVTR.Lodging.Context
{
  /// <summary>
  /// Represents the _Lodging_ context
  /// </summary>
  public class LodgingContext : DbContext
  {
    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    public DbSet<LodgingModel> Lodgings { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    public DbSet<RentalModel> Rentals { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    public DbSet<ReviewModel> Reviews { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    public DbSet<ImageModel> Images { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public LodgingContext(DbContextOptions<LodgingContext> options) : base(options) { }

    /// <summary>
    ///
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<AddressModel>().HasKey(e => e.EntityId);
      modelBuilder.Entity<LodgingModel>().HasKey(e => e.EntityId);
      modelBuilder.Entity<RentalModel>().HasKey(e => e.EntityId);
      modelBuilder.Entity<ReviewModel>().HasKey(e => e.EntityId);
      modelBuilder.Entity<ImageModel>().HasKey(e => e.EntityId);

      OnDataSeeding(modelBuilder);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="modelBuilder"></param>
    private void OnDataSeeding(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<LodgingModel>().HasData(new List<LodgingModel>()
      {
        new LodgingModel() { EntityId = 1, AddressId = 1, Name = "Dragon Fly", Bathrooms = 2 },
        new LodgingModel() { EntityId = 2, AddressId = 2, Name = "Galleywinter", Bathrooms = 3 },
        new LodgingModel() { EntityId = 3, AddressId = 3, Name = "Red Creek", Bathrooms = 5 },
        new LodgingModel() { EntityId = 4, AddressId = 4, Name = "Lotus Belle", Bathrooms = 6 },
      });

      modelBuilder.Entity<RentalModel>().HasData(new List<RentalModel>()
      {
        new RentalModel() { EntityId = 1, LodgingModelId = 1, LotNumber = "100", Status = "Available", Price = 100, DiscountedPrice = 70, Capacity = 4, SiteName = "Tent", Size = "5x5" },
        new RentalModel() { EntityId = 2, LodgingModelId = 1, LotNumber = "101", Status = "Available", Price = 300, DiscountedPrice = 280, Capacity = 5, SiteName = "RV", Size = "10x10" },
        new RentalModel() { EntityId = 3, LodgingModelId = 1, LotNumber = "102", Status = "Booked", Price = 300, DiscountedPrice = 280, Capacity = 5, SiteName = "RV", Size = "10x10" },
        new RentalModel() { EntityId = 4, LodgingModelId = 1, LotNumber = "103", Status = "Booked", Price = 100, DiscountedPrice = 70, Capacity = 4, SiteName = "Tent", Size = "5x5" },
        new RentalModel() { EntityId = 5, LodgingModelId = 2, LotNumber = "100", Status = "Available", Price = 100, DiscountedPrice = 70, Capacity = 4, SiteName = "Tent", Size = "5x5" },
        new RentalModel() { EntityId = 6, LodgingModelId = 2, LotNumber = "101", Status = "Available", Price = 300, DiscountedPrice = 280, Capacity = 5, SiteName = "RV", Size = "10x10" },
        new RentalModel() { EntityId = 7, LodgingModelId = 2, LotNumber = "102", Status = "Booked", Price = 300, DiscountedPrice = 280, Capacity = 5, SiteName = "RV", Size = "10x10" },
        new RentalModel() { EntityId = 8, LodgingModelId = 2, LotNumber = "103", Status = "Booked", Price = 100, DiscountedPrice = 70, Capacity = 4, SiteName = "Tent", Size = "5x5" },
        new RentalModel() { EntityId = 9, LodgingModelId = 3, LotNumber = "100", Status = "Available", Price = 100, DiscountedPrice = 70, Capacity = 4, SiteName = "Tent", Size = "5x5" },
        new RentalModel() { EntityId = 10, LodgingModelId = 3, LotNumber = "101", Status = "Booked", Price = 100, DiscountedPrice = 70, Capacity = 5, SiteName = "Tent", Size = "5x5"  },
        new RentalModel() { EntityId = 11, LodgingModelId = 4, LotNumber = "100", Status = "Available", Price = 300, DiscountedPrice = 280, Capacity = 4, SiteName = "RV", Size = "10x10" },
        new RentalModel() { EntityId = 12, LodgingModelId = 4, LotNumber = "101", Status = "Booked", Price = 300, DiscountedPrice = 280, Capacity = 5, SiteName = "RV", Size = "10x10" },
      });

      modelBuilder.Entity<ImageModel>().HasData(new List<ImageModel>()
      {
        new ImageModel() {EntityId = 1, LodgingModelId = 1, ImageUri = "https://images.pexels.com/photos/417074/pexels-photo-417074.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500"},
        new ImageModel() {EntityId = 2, LodgingModelId = 1, ImageUri = "https://3.bp.blogspot.com/-ha7tdQrX-NU/WW8YoM1EqwI/AAAAAAAAGXo/-8t35xR6DmsvhSQmuufNy020Jarn9FZYQCHMYBhgL/s1600/pc-nature-wallpapers-cnsoup-collections.jpg"},
        new ImageModel() {EntityId = 3, LodgingModelId = 1, ImageUri = "https://swall.teahub.io/photos/small/323-3236437_nature-wallpaper-pc-4k.jpg"},
        new ImageModel() {EntityId = 4, LodgingModelId = 1, ImageUri = "https://w.wallhaven.cc/full/md/wallhaven-md5do1.jpg"},
        new ImageModel() {EntityId = 5, LodgingModelId = 1, ImageUri = "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcRQPXqYYN-xTlO2yPaNK8b1GW3m4kQlbKkRhA&usqp=CAU"},
        new ImageModel() {EntityId = 6, LodgingModelId = 1, ImageUri = "https://t4.ftcdn.net/jpg/02/91/24/27/360_F_291242770_z3XC7rJB1Mvc5jVMsEY9Dx2xMrX4sxUi.jpg"},
        new ImageModel() {EntityId = 7, LodgingModelId = 1, ImageUri = "https://croatia.hr/sites/default/files/styles/image_full_width/public/2020-01/Plitvice-Lakes-National-Park_shutterstock_454592293_1600x900.jpg?itok=xwQuMGjg"},

        new ImageModel() {EntityId = 8, LodgingModelId = 2, ImageUri = "https://www.nps.gov/lacl/learn/nature/images/LACL_2017_Entering-the-Neacolas-from-Big-Valley_LWilcox.JPG"},
        new ImageModel() {EntityId = 9, LodgingModelId = 2, ImageUri = "https://media.nationalgeographic.org/assets/photos/201/399/978429cd-0d04-464b-a3c1-473e163ecc5b.jpg"},
        new ImageModel() {EntityId = 10, LodgingModelId = 2, ImageUri = "https://res.cloudinary.com/dk-find-out/image/upload/q_80,w_1920,f_auto/Tundra_Scene1_okuw9s.jpg"},
        new ImageModel() {EntityId = 11, LodgingModelId = 2, ImageUri = "https://www.thoughtco.com/thmb/BCx7nDQlalfdT1iXhSeL01Ha454=/1333x1000/smart/filters:no_upscale()/92292471-56a005035f9b58eba4ae83e0.jpg"},
        new ImageModel() {EntityId = 12, LodgingModelId = 2, ImageUri = "https://upload.wikimedia.org/wikipedia/commons/a/ac/Nunavut_tundra_-c.jpg"},
        new ImageModel() {EntityId = 13, LodgingModelId = 2, ImageUri = "https://nhpbs.org/wild/images/muskoxusfwtimBowman.jpg"},
        new ImageModel() {EntityId = 14, LodgingModelId = 2, ImageUri = "https://images.fineartamerica.com/images-medium-large-5/3-bull-caribou-on-autumn-tundra-in-denali-milo-burcham.jpg"},

        new ImageModel() {EntityId = 15, LodgingModelId = 3, ImageUri = "https://eskipaper.com/images/awesome-jungle-wallpaper-2.jpg"},
        new ImageModel() {EntityId = 16, LodgingModelId = 3, ImageUri = "https://images.unsplash.com/photo-1588392382834-a891154bca4d?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&w=1000&q=80"},
        new ImageModel() {EntityId = 17, LodgingModelId = 3, ImageUri = "https://wallpapermemory.com/uploads/274/jungle-wallpaper-hd-1120x832-447114.jpg"},
        new ImageModel() {EntityId = 18, LodgingModelId = 3, ImageUri = "https://images.alphacoders.com/708/thumb-1920-708947.jpg"},
        new ImageModel() {EntityId = 19, LodgingModelId = 3, ImageUri = "https://www.worldwalks.com/wp-content/uploads/2018/12/Amazon_870-870x480.jpg"},
        new ImageModel() {EntityId = 20, LodgingModelId = 3, ImageUri = "https://www.goodnewsnetwork.org/wp-content/uploads/2014/06/Potaro-river-Kaieteur-falls-Amazon-cc-Allan_Hopkins.jpg"},
        new ImageModel() {EntityId = 21, LodgingModelId = 3, ImageUri = "https://imagesvc.meredithcorp.io/v3/mm/image?url=https%3A%2F%2Fstatic.onecms.io%2Fwp-content%2Fuploads%2Fsites%2F28%2F2020%2F06%2F18%2Folympia-national-park-hoh-campground-NPCAMP0620.jpg"},

        new ImageModel() {EntityId = 22, LodgingModelId = 4, ImageUri = "https://cdn.mos.cms.futurecdn.net/deaceNXy23NF8VsCrwZPgn.jpg"},
        new ImageModel() {EntityId = 23, LodgingModelId = 4, ImageUri = "https://geographical.co.uk/media/k2/items/cache/a74a4e8d02982a465da28ab5257d0d4d_XL.jpg"},
        new ImageModel() {EntityId = 24, LodgingModelId = 4, ImageUri = "https://i.pinimg.com/originals/e5/cc/49/e5cc49acade6f109f241e8206ade70f0.jpg"},
        new ImageModel() {EntityId = 25, LodgingModelId = 4, ImageUri = "https://images.fineartamerica.com/images/artworkimages/mediumlarge/1/colorful-sunset-in-the-desert-anton-petrus.jpg"},
        new ImageModel() {EntityId = 26, LodgingModelId = 4, ImageUri = "https://www.turbopass.com/3843-carousel/desert-sunset-experience.jpg"},
        new ImageModel() {EntityId = 27, LodgingModelId = 4, ImageUri = "https://www.kcet.org/sites/kl/files/atoms/article_atoms/www.kcet.org/living/travel/socal_wanderer/jumbo-rocks-campground-joshua-tree.jpeg"},
        new ImageModel() {EntityId = 28, LodgingModelId = 4, ImageUri = "https://upload.wikimedia.org/wikipedia/commons/6/67/Desert_Fox_Pups.jpg"},
      });

      modelBuilder.Entity<AddressModel>().HasData(new List<AddressModel>()
      {
        new AddressModel() { EntityId = 1, City = "Palm Bay", Country = "USA", Latitude = "38.0755", Longitude = "77.9889", PostalCode = "32908", StateProvince = "FL", Street = "750 Osmosis Dr SW" },
        new AddressModel() { EntityId = 2, City = "Afton", Country = "USA", Latitude = "38.0755", Longitude = "77.9889", PostalCode = "22920", StateProvince = "VA", Street = "8801 Dick Woods Rd" },
        new AddressModel() { EntityId = 3, City = "Hanna", Country = "USA", Latitude = "38.0755", Longitude = "77.9889", PostalCode = "84031", StateProvince = "UT", Street = "5761 Upper, Red Creek Rd" },
        new AddressModel() { EntityId = 4, City = "Topanga", Country = "USA", Latitude = "38.0755", Longitude = "77.9889", PostalCode = "90290", StateProvince = "CA", Street = "101 S Topanga Canyon Blvd" },
      });
    }
  }
}
